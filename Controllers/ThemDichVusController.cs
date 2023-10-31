using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemDichVusController : ControllerBase
    {
        private readonly IThemDichVuService _themDichVus;

        public ThemDichVusController(IThemDichVuService themDichVus)
        {
            _themDichVus = themDichVus;
        }

        // GET: api/ThemDichVus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemDichVuResponeDto>>> GetThemDichVus()
        {
            if (_themDichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity suKienThemDichVu don't exist"
                    }
                });
            }
            var dichVus = await _themDichVus.getThemDichVus();
            return Ok(new Result() { Success = true, Data = dichVus });
        }

        // GET: api/ThemDichVus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemDichVuResponeDto>> GetThemDichVu(int id)
        {
            if (_themDichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity SuKienThemDichVu don't exist"
                    }
                });
            }
            var themDichVu = await _themDichVus.getThemDichVu(id);

            if (themDichVu == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "themDichVu don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = themDichVu
            });
        }

        // PUT: api/ThemDichVu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchThemDichVu(int id, [FromBody] ThemDichVuRequestDto dp)
        {
            var themDichVu = await _themDichVus.getThemDichVu(id);
            if (themDichVu == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "ThemDichVu not exist",
                    }
                });
            }
            var successUpdate = await _themDichVus.patchThemDichVu(id, dp);
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
            return NoContent();
        }

        // POST: api/ThemDichVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DichVuResponeDto>> PostDichVu([FromBody] ThemDichVuRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                if (_themDichVus == null)
                {
                    return Problem("Entity set 'DataContext.ThemDichVu'  is null.");
                }
                var successCreated = await _themDichVus.postThemDichVu(dto);
                if (successCreated == null)
                {
                    return BadRequest(new Result()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Don't created a ThemPhuPhi",
                            "trang thai có thể đã bị sai"
                        }
                    });
                }
                return CreatedAtAction("GetThemDichVu", new { id = successCreated.MaSK }, new Result()
                {
                    Success = true,
                    Data = successCreated
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

        // DELETE: api/ThemDichVus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThemDichVu(int id)
        {
            if (_themDichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity ThemDichVu don't exist"
                    }
                });
            }
            var themDichVu = await _themDichVus.getThemDichVu(id);
            if (themDichVu== null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "themDichVu don't exist"
                    }
                });
            }
            await _themDichVus.deleteThemDichVu(id);
            return NoContent();
        }
    }
}
