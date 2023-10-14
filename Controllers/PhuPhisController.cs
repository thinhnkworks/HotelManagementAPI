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
    public class PhuPhisController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PhuPhisController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/PhuPhis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhuPhiResponeDto>>> GetPhuPhis()
        {
            if (_unitOfWork.PhuPhis == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity PhuPhi don't exist"
                    }
                });
            }
            var phuPhis = _mapper.Map<List<PhuPhiResponeDto>>(await _unitOfWork.PhuPhis.GetAllAsync());
            return Ok(new Result() { Success = true, Data = phuPhis });
        }

        // GET: api/PhuPhis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongResponeDto>> GetPhuPhi(int id)
        {
            if (_unitOfWork.PhuPhis == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity PhuPhi don't exist"
                    }
                });
            }
            var phuPhi = await _unitOfWork.PhuPhis.GetAsync(id);

            if (phuPhi == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "phuPhi don't exist"
                    }
                });
            }
            return Ok(new Result()
            {
                Success = true,
                Data = _mapper.Map<PhuPhiResponeDto>(phuPhi)
            });
        }

        // PUT: api/PhuPhis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPhuPhi(int id, [FromBody] PhuPhiRequestDto phuPhi)
        {
            var existPhuPhi = await _unitOfWork.PhuPhis.GetAsync(id);
            if (existPhuPhi == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "phuPhi not exist",
                    }
                });
            }
            var convertToPhPhi = _mapper.Map<PhuPhi>(phuPhi);
            var successUpdate = await _unitOfWork.PhuPhis.UpdateAsync(id, convertToPhPhi);
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

        // POST: api/PhuPhis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhuPhiResponeDto>> PostPhuPhi([FromBody] PhuPhiRequestDto phuPhi)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.PhuPhis == null)
                {
                    return Problem("Entity set 'DataContext.PhuPhis'  is null.");
                }
                var convertToPhuPhi = _mapper.Map<PhuPhi>(phuPhi);
                var successCreated = await _unitOfWork.PhuPhis.AddAsync(convertToPhuPhi);
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
                return CreatedAtAction("GetPhuPhi", new { id = convertToPhuPhi.MaPp }, new Result()
                {
                    Success = true,
                    Data = _mapper.Map<PhuPhiResponeDto>(convertToPhuPhi)
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

        // DELETE: api/PhuPhis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhuPhi(int id)
        {
            if (_unitOfWork.Phongs == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity PhuPhi don't exist"
                    }
                });
            }
            var phuPhi = await _unitOfWork.PhuPhis.GetAsync(id);
            if (phuPhi == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "phuPhi don't exist"
                    }
                });
            }
            await _unitOfWork.PhuPhis.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
