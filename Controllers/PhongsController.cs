using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using AutoMapper;
using Sieve.Services;
using HotelManagementAPI.Core.Repositories;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class PhongsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;

        public PhongsController(IUnitOfWork unitOfWork, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        // GET: api/Phongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongResponeDto>>> GetPhongs()
        {
          if (_unitOfWork.Phongs == null)
          {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Phong don't exist"
                    }
                });
          }
          var phongs = _mapper.Map<List<PhongResponeDto>>(await  _unitOfWork.Phongs.GetAllAsync());
            return Ok(new Result() { Success = true, Data = phongs });
        }

        // GET: api/Phongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongResponeDto>> GetPhong(int id)
        {
          if (_unitOfWork.Phongs == null)
          {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Phong don't exist"
                    }
                });
            }
            var phong = await _unitOfWork.Phongs.GetAsync(id);

            if (phong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Phong don't exist"
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
                Data = _mapper.Map<PhongResponeDto>(phong)
            });
        }

        // PUT: api/Phongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPhong(int id, [FromBody] PhongRequestDto phong)
        {
            var existPhong = await _unitOfWork.Phongs.GetAsync(id);
            if (existPhong == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "phong not exist",
                    }
                });
            }
            var convertToPhong = _mapper.Map<Phong>(phong);
            var successUpdate = await _unitOfWork.Phongs.UpdateAsync(id, convertToPhong);
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

        // POST: api/Phongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhongResponeDto>> PostPhong([FromBody] PhongRequestDto phong)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Phongs == null)
                {
                    return Problem("Entity set 'DataContext.Phongs'  is null.");
                }
                var convertToPhong = _mapper.Map<Phong>(phong);
                var successCreated = await _unitOfWork.Phongs.AddAsync(convertToPhong);
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
                return CreatedAtAction("GetPhong", new { id = convertToPhong.MaPhong }, new Result()
                {
                    Success = true,
                    Data = _mapper.Map<PhongResponeDto>(convertToPhong)
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

        // DELETE: api/Phongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhong(int id)
        {
            if (_unitOfWork.Phongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Phong don't exist"
                    }
                });
            }
            var phong = await _unitOfWork.Phongs.GetAsync(id);
            if (phong == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "phong don't exist"
                    }
                });
            }
            await _unitOfWork.Phongs.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
