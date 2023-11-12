using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Services.IService;
using HotelManagementAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _hoaDons;

        public HoaDonController(IHoaDonService hoaDons)
        {
           _hoaDons = hoaDons;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDonResponeDto>>> GetHoaDons()
        {
            if (_hoaDons == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity hoaDon don't exist"
                    }
                });
            }
            var hoaDons = await _hoaDons.getHoaDons();
            return Ok(new Result() { Success = true, Data = hoaDons });
        }

        // GET: api/hoaDon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonResponeDto>> GetHoaDon(int id, [FromQuery] int? MaPhong, [FromQuery] bool? check = false)
        {
            if (_hoaDons == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity hoaDon don't exist"
                    }
                });
            }
            var hoaDon = await _hoaDons.getHoaDon(id, check, MaPhong);

            if (hoaDon == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "hoaDon don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = hoaDon
            });
        }

     
        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            if (_hoaDons == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity hoa Don don't exist"
                    }
                });
            }
            var hoaDon = await _hoaDons.deleteHoaDon(id);
            if (hoaDon == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "hoaDOn don't exist"
                    }
                });
            }
            await _hoaDons.deleteHoaDon(id);
            return NoContent();
        }
    }
}
