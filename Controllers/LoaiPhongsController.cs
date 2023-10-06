using AutoMapper;
using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Services;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiPhongsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;

        public LoaiPhongsController(IUnitOfWork unitOfWork, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        // GET: api/LoaiPhongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiPhongResponeDto>>> GetLoaiPhongs()
        {
            if (_unitOfWork.LoaiPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity LoaiPhong don't exist"
                    }
                });
            }
            var loaiPhongs = _mapper.Map<List<LoaiPhongResponeDto>>(await _unitOfWork.LoaiPhongs.GetAllAsync());
            return Ok(new Result() { Success = true, Data = loaiPhongs });
        }
        // GET: api/LoaiPhongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongResponeDto>> GetLoaiPhong(int id)
        {
            if (_unitOfWork.LoaiPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity LoaiPhong don't exist"
                    }
                });
            }
            var loaiPhong = await _unitOfWork.LoaiPhongs.GetAsync(id);

            if (loaiPhong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "LoaiPhong don't exist"
                    }
                });
            }
            /*var newPhong = new PhongResponeDto()
            {
                LoaiPhong = _mapper.Map<LoaiPhongResponeDto>(phong.MaLoaiPhongNavigation),
                MaPhong = phong.MaPhong,
                TrangThai = phong.TrangThai,
            };*/
            return Ok(new Result()
            {
                Success = true,
                Data = _mapper.Map<LoaiPhongResponeDto>(loaiPhong)
            });
        }
        // PUT: api/LoaiPhongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLoaiPhong(int id, [FromBody] LoaiPhongRequestDto loaiPhong)
        {
            var existLoaiPhong = await _unitOfWork.LoaiPhongs.GetAsync(id);
            if (existLoaiPhong == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "loaiphong not exist",
                    }
                });
            }
            var convertToLoaiPhong = _mapper.Map<LoaiPhong>(loaiPhong);
            var successUpdate = await _unitOfWork.LoaiPhongs.UpdateAsync(id, convertToLoaiPhong);
            if (!successUpdate)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>()
                        {
                            "Invalid input, There is a field that has not been validated",
                        }
                });
            }
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // POST: api/LoaiPhongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhongResponeDto>> PostLoaiPhong([FromBody] LoaiPhongRequestDto loaiPhong)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.LoaiPhongs == null)
                {
                    return Problem("Entity set 'DataContext.Phongs'  is null.");
                }
                var convertToLoaiPhong = _mapper.Map<LoaiPhong>(loaiPhong);
                var successCreated = await _unitOfWork.LoaiPhongs.AddAsync(convertToLoaiPhong);
                if (successCreated == false)
                {
                    return BadRequest(new Result()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Invalid input, There is a field that has not been validated",
                        }
                    });
                }
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction("GetLoaiPhong", new { id = convertToLoaiPhong.MaLoaiPhong }, new Result()
                {
                    Success = true,
                    Data = _mapper.Map<LoaiPhongResponeDto>(convertToLoaiPhong)
                });
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

        // DELETE: api/LoaiPhongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiPhong(int id)
        {
            if (_unitOfWork.LoaiPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Loai Phong don't exist"
                    }
                });
            }
            var loaiPhong= await _unitOfWork.LoaiPhongs.GetAsync(id);
            if (loaiPhong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "loaiphong don't exist"
                    }
                });
            }
            await _unitOfWork.LoaiPhongs.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

    }
}
