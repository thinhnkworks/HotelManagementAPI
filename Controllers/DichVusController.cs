using AutoMapper;
using HotelManagementAPI.Data;
using HotelManagementAPI.DTO.Request;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public DichVusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/DichVus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVuResponeDto>>> GetDichVus()
        {
            if (_unitOfWork.DichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity DichVu don't exist"
                    }
                });
            }
            var dichVus = _mapper.Map<List<DichVuResponeDto>>(await _unitOfWork.DichVus.GetAllAsync());
            return Ok(new Result() { Success = true, Data = dichVus });
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVuResponeDto>> GetDichVu(int id)
        {
            if (_unitOfWork.DichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity DichVu don't exist"
                    }
                });
            }
            var dichVu = await _unitOfWork.DichVus.GetAsync(id);

            if (dichVu == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "dichVu don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = _mapper.Map<DichVuResponeDto>(dichVu)
            });
        }

        // PUT: api/DichVus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDichVu(int id, [FromBody] DichVuRequestDto dichVu)
        {
            var existDichVu = await _unitOfWork.DichVus.GetAsync(id);
            if (existDichVu == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "dichvu not exist",
                    }
                });
            }
            var convertToDichVu = _mapper.Map<DichVu>(dichVu);
            var successUpdate = await _unitOfWork.DichVus.UpdateAsync(id, convertToDichVu);
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

        // POST: api/DichVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DichVuResponeDto>> PostPhuPhi([FromBody] DichVuRequestDto dichVu)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.DichVus == null)
                {
                    return Problem("Entity set 'DataContext.DichVus'  is null.");
                }
                var convertToDichVu= _mapper.Map<DichVu>(dichVu);
                var successCreated = await _unitOfWork.DichVus.AddAsync(convertToDichVu);
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
                return CreatedAtAction("GetDichVu", new { id = convertToDichVu.MaDv }, new Result()
                {
                    Success = true,
                    Data = _mapper.Map<DichVuResponeDto>(convertToDichVu)
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
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            if (_unitOfWork.DichVus == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity DichVu don't exist"
                    }
                });
            }
            var dichVu = await _unitOfWork.DichVus.GetAsync(id);
            if (dichVu == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "dichVu don't exist"
                    }
                });
            }
            await _unitOfWork.DichVus.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
