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
    public class ThemPhuPhisController : ControllerBase
    {
        private readonly IThemPhuPhiService _themPhuPhis;

        public ThemPhuPhisController(IThemPhuPhiService themPhuPhis)
        {
            _themPhuPhis = themPhuPhis;
        }

        // GET: api/ThemPhuPhis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemPhuPhiResponeDto>>> GetThemPhuPhis()
        {
            if (_themPhuPhis == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity suKienPhuPhi don't exist"
                    }
                });
            }
            var phuPhis = await _themPhuPhis.getThemPhuPhis();
            return Ok(new Result() { Success = true, Data = phuPhis });
        }

        // GET: api/PhuPhis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemPhuPhiResponeDto>> GetThemPhuPhi(int id)
        {
            if (_themPhuPhis == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity SuKienThemPhuPhi don't exist"
                    }
                });
            }
            var themPhuPhi = await _themPhuPhis.getThemPhuPhi(id);

            if (themPhuPhi == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "themphuphi don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = themPhuPhi
            });
        }

        // PUT: api/ThemPhuPhis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchThemPhuPhi(int id, [FromBody] ThemPhuPhiRequestDto dp)
        {
            var themPhuPhi = await _themPhuPhis.getThemPhuPhi(id);
            if (themPhuPhi == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "ThemPhuPhi not exist",
                    }
                });
            }
            var successUpdate = await _themPhuPhis.patchThemPhuPhi(id, dp);
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

        // POST: api/ThemPhuPhis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DatPhongResponeDto>> PostDatPhong([FromBody] ThemPhuPhiRequestDto dto)
        {
            if (ModelState.IsValid)
            {
                if (_themPhuPhis == null)
                {
                    return Problem("Entity set 'DataContext.ThemPhuPhis'  is null.");
                }
                var successCreated = await _themPhuPhis.postThemPhuPhi(dto);
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
                return CreatedAtAction("GetThemPhuPhi", new { id = successCreated.MaSK }, new Result()
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

        // DELETE: api/ThemPhuPhis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThemPhuPhi(int id)
        {
            if (_themPhuPhis == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity ThemPhuPhi don't exist"
                    }
                });
            }
            var themPhuPhi = await _themPhuPhis.getThemPhuPhi(id);
            if (themPhuPhi == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "themPhuPhi don't exist"
                    }
                });
            }
            await _themPhuPhis.deleteThemPhuPhi(id);
            return NoContent();
        }
    }
}
