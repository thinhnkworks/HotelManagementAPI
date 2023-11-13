using AutoMapper;
using HotelManagementAPI.Configuration;
using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;

        public AuthManagementController(IUnitOfWork unitOfWork, IMapper mapper, IOptionsMonitor<JwtConfig> jwtConfig, TokenValidationParameters tokenValidationParameters)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtConfig = jwtConfig.CurrentValue;
            _tokenValidationParams = tokenValidationParameters;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUserByCCCD = await _unitOfWork.NhanViens.GetByCCCDAsync(user.Cccd!);
                if (existingUserByCCCD != null)
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Errors = new List<string>() {
                            "CCCD already in use"
                    },
                        Success = false
                    });
                }
                var existingUserBySDT = await _unitOfWork.NhanViens.GetBySdtAsync(user.Sdt!);
                if (existingUserBySDT != null)
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Errors = new List<string>() {
                            "SDT already in use"
                },
                        Success = false
                    });
                }
                var newNhanVien = new NhanVien()
                {
                    Cccd = user.Cccd,
                    DiaChi = user.DiaChi,
                    GioiTinh = user.GioiTinh,
                    HoTen = user.HoTen,
                    NgaySinh = user.NgaySinh,
                    Sdt = user.Sdt,
                    QuanLy = false
                };
                var isCreated = await _unitOfWork.NhanViens.CreateANhanVienWithHashPassword(newNhanVien, user.MatKhau);
                if (isCreated)
                {
                    await _unitOfWork.CompleteAsync();
                    // var result = await _userManager.AddToRoleAsync(newUser, "AppUser");
                    var jwt = GenerateJwtToken(newNhanVien, false);
                    return Ok(jwt);
                }
                else
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Errors = new List<string>
                        {
                            "Co loi trong khi dang ky hoac tao token"
                        },
                        Success = false
                    });
                }
            }
            return new JsonResult(new Result()
            {
                Success = false,
                Errors = new List<string>() {
                        "Something went wrong",
                }
            })
            { StatusCode = 500 };
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dataUser)
        {
            if (ModelState.IsValid)
            {
                var existUser = await _unitOfWork.NhanViens.GetByCCCDAsync(dataUser.Cccd!);
                if (existUser == null)
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "invalid login request",
                        }
                    });
                }
                if(existUser.QuanLy != dataUser.IsAdmin!)
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Khong dung role",
                        }
                    });
                }
                var checkPassword = _unitOfWork.NhanViens.CheckPassword(existUser, dataUser.Password!);
                if (!checkPassword)
                {
                    return BadRequest(new RegistrationRespone()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "invalid login request"
                        }
                    });
                }
                var jwt = GenerateJwtToken(existUser, dataUser.IsAdmin);

                return Ok(jwt);
            }

            return BadRequest(new RegistrationRespone()
            {
                Success = false,
                Errors = new List<string>() {
                    "Invalid payload",
                }
            });
        }
        [HttpGet]
        [Route("my")]
        [Authorize]
        public async Task<ActionResult<NhanVienResponeDto>> GetMyCurrentUser()
        {
            var user = HttpContext.User;
            if (user != null && user.Identity!.IsAuthenticated)
            {
                var CCCD = user!.FindFirst("CCCD")?.Value;
                if (!string.IsNullOrEmpty(CCCD))
                {
                    var userData = await _unitOfWork.NhanViens.GetByCCCDAsync(CCCD);
                    if (userData != null)
                    {
                        return Ok(new Result() { Success = true, Data = _mapper.Map<NhanVienResponeDto>(userData) });
                    }
                    return NotFound(new Result()
                    {
                        Success = false,
                        Errors = new List<string>(){
                        "User does not exist"
                    }
                    });
                }
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>(){
                        "Id did not found"
                    }
                });
            }
            else
            {
                return Unauthorized();
            }
        }
        private List<Claim> getAllValidClaims(NhanVien nhanvien, bool QuanLy)
        {
            var _options = new IdentityOptions();
            var claims = new List<Claim>() {

                  new Claim("Id", (nhanvien.MaNv).ToString()),
                  new Claim(JwtRegisteredClaimNames.NameId, (nhanvien.MaNv!).ToString()),
                  new Claim("CCCD", nhanvien.Cccd!),
                  new Claim(JwtRegisteredClaimNames.Name, nhanvien.HoTen!),
                  new Claim(JwtRegisteredClaimNames.Sub, nhanvien.Cccd!),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };
            if (QuanLy)
            {
                claims.Add(new Claim(ClaimTypes.Role, RoleNhanVien.User));
                claims.Add(new Claim(ClaimTypes.Role, RoleNhanVien.Admin));
            }
            else {
                claims.Add(new Claim(ClaimTypes.Role, RoleNhanVien.User));
            }
            return claims;
        }
        private  AuthResult GenerateJwtToken(NhanVien user, bool QuanLy)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig!.Secret!);
            var claims = getAllValidClaims(user, QuanLy);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };
            var token = jwtTokenHandler.CreateToken(tokenDecriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

           
            return new RegistrationRespone()
            {
                Token = jwtToken,
                Success = true
            };
        }
    } 
}
