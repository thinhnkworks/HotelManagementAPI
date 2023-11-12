using AutoMapper;
using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Models;
using HotelManagementAPI.Services.IService;
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
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongs;

        public DatPhongController(IDatPhongService datPhongs)
        {
            _datPhongs = datPhongs;
        }

        // GET: api/DatPhongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatPhongResponeDto>>> GetDatPhongs()
        {
            if (_datPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity datPhong don't exist"
                    }
                });
            }
            var datPhongs = await _datPhongs.getDatPhongs();
            return Ok(new Result() { Success = true, Data = datPhongs });
        }

        // GET: api/DatPhong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DatPhongResponeDto>> GetDatPhong(int id)
        {
            if (_datPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity DatPhong don't exist"
                    }
                });
            }
            var datPhong = await _datPhongs.getDatPhong(id);

            if (datPhong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "datphong don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = datPhong
            });
        }

        // PUT: api/DatPhongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDatPhong(int id, [FromBody] DatPhongRequestDto dp)
        {
            var datPhong = await _datPhongs.getDatPhong(id);
            if (datPhong == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "dat phong not exist",
                    }
                });
            }
            var successUpdate = await _datPhongs.patchDatPhong(id, dp);
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

        // POST: api/DatPhongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DatPhongResponeDto>> PostDatPhong([FromBody] DatPhongRequestDto datPhong)
        {
            if (ModelState.IsValid)
            {
                if (_datPhongs == null)
                {
                    return Problem("Entity set 'DataContext.DatPhongs'  is null.");
                }
                var successCreated = await _datPhongs.postDatPhong(datPhong);
                if (successCreated == null)
                {
                    return BadRequest(new Result()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Don't created a skDatPhong",
                            "trang thai có thể đã bị sai"
                        }
                    });
                }
                return CreatedAtAction("GetDatPhong", new { id = successCreated.MaSK }, new Result()
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

        // DELETE: api/DichVus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatPhong(int id)
        {
            if (_datPhongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity DatPhong don't exist"
                    }
                });
            }
            var datPhong = await _datPhongs.getDatPhong(id);
            if (datPhong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "datPhong don't exist"
                    }
                });
            }
            await _datPhongs.deleteDatPhong(id);
            return NoContent();
        }
    }
}
