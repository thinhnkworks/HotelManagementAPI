using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using HotelManagementAPI.DTO.Request;
using AutoMapper;
using HotelManagementAPI.DTO.Respone;
using HotelManagementAPI.DTO.Result;
using Sieve.Models;
using Sieve.Services;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;

        public NhanViensController(IUnitOfWork unitOfWork, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        // GET: api/NhanViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVienResponeDto>>> GetNhanViens([FromQuery] SieveModel model)
        {
            if (_unitOfWork.NhanViens == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity NhanVien don't exist"
                    }
                });
            }
            var nhanViens = (await _unitOfWork.NhanViens.GetAllAsync()).AsQueryable();
            nhanViens = _sieveProcessor.Apply(model, nhanViens);
            return Ok(new Result()
            {
                Success = true,
                Data = _mapper.Map<List<NhanVienResponeDto>>(nhanViens)
            });
        }

        // GET: api/NhanViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVienResponeDto>> GetNhanVien(int id)
        {
            if (_unitOfWork.NhanViens == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity NhanVien don't exist"
                    }
                });
            }
            var nhanVien = await _unitOfWork.NhanViens.GetAsync(id);

            if (nhanVien == null)
            {

                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "NhanVien don't exist"
                    }
                });
            }
            return Ok(new Result() { Success = true, Data = _mapper.Map<NhanVienResponeDto>(nhanVien) });
        }

        // GET: api/NhanViens/sdt/1234567891
        [HttpGet("sdt/{sdt}")]
        public async Task<ActionResult<NhanVienResponeDto>> GetNhanVien(string sdt)
        {
            if (_unitOfWork.NhanViens == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity NhanVien don't exist"
                    }
                });
            }
            var nhanVien = await _unitOfWork.NhanViens.GetBySdtAsync(sdt);

            if (nhanVien == null)
            {

                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Nhanvien don't exist"
                    }
                });
            }
            return Ok(new Result() { Success = true, Data = _mapper.Map<NhanVienResponeDto>(nhanVien) });
        }
        // PUT: api/NhanViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchNhanVien(int id, [FromBody] NhanVienRequestDto nhanVien)
        {
            var existNhanVien = await _unitOfWork.NhanViens.GetAsync(id);
            if (existNhanVien == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "nhanvien not exist",
                    }
                });
            }
            var password = nhanVien.MatKhau;
            var convertToNhanVien = _mapper.Map<NhanVien>(nhanVien);
            var successUpdate = await _unitOfWork.NhanViens.UpdateNhanVienWithHashPassword(id, convertToNhanVien, password);
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

        // POST: api/NhanViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhanVienResponeDto>> PostNhanVien(NhanVienRequestDto nhanVien)
        {
            if (_unitOfWork.NhanViens == null)
            {
                return Problem("Entity set 'DataContext.NhanViens'  is null.");
            }
            var convertNhanVien = _mapper.Map<NhanVien>(nhanVien);
            var successCreated = await _unitOfWork.NhanViens.CreateANhanVienWithHashPassword(convertNhanVien, nhanVien.MatKhau);
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
            return CreatedAtAction("GetNhanVien", new { id = convertNhanVien.MaNv }, new Result()
            {
                Success = true,
                Data = _mapper.Map<NhanVienResponeDto>(convertNhanVien)
            });
        }

        // DELETE: api/NhanViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            if (_unitOfWork.NhanViens == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity NhanVien don't exist"
                    }
                });
            }
            var nhanVien = await _unitOfWork.NhanViens.GetAsync(id);
            if (nhanVien == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "nhanvien don't exist"
                    }
                });
            }
            await _unitOfWork.NhanViens.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
