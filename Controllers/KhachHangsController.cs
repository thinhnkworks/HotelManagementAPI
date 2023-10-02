using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using HotelManagementAPI.DTO.Respone;
using AutoMapper;
using HotelManagementAPI.DTO.Result;
using HotelManagementAPI.DTO.Request;
using Sieve.Services;
using Sieve.Models;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;

        public KhachHangsController(IUnitOfWork unitOfWork, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        // GET: api/KhachHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHangResponeDto>>> GetKhachHangs([FromQuery] SieveModel model)
        {
          if (_unitOfWork.Customers == null)
          {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Customer don't exist"
                    }
                });
          }
            var khachHangs = (await _unitOfWork.Customers.GetAllAsync()).AsQueryable();
            khachHangs = _sieveProcessor.Apply(model, khachHangs);
            var khacHangsDto = _mapper.Map<List<KhachHangResponeDto>>(khachHangs);
            return Ok(new Result()
            {
                Success = true,
                Data = khacHangsDto
            }) ;
        }

        // GET: api/KhachHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHangResponeDto>> GetKhachHang(int id)
        {
          if (_unitOfWork.Customers == null)
          {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Customer don't exist"
                    }
                });
            }
            var khachHang = await _unitOfWork.Customers.GetAsync(id);

            if (khachHang == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Customer don't exist"
                    }
                });
            }
            return Ok(_mapper.Map<KhachHangResponeDto>(khachHang));
        }

        // PUT: api/KhachHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchKhachHang(int id,[FromBody] KhachHangRequestUpdateDto khachHang)
        {
            var existKhachHang = await _unitOfWork.Customers.GetAsync(id);
            if(existKhachHang == null)
            {
                return BadRequest(new Result()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "user not exist",
                    }
                });
            }
            var convertToKhachHang = _mapper.Map<KhachHang>(khachHang);
            await _unitOfWork.Customers.UpdateAsync(convertToKhachHang);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // POST: api/KhachHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang([FromBody] KhachHangRequestPostDto khachHang)
        {
          if(ModelState.IsValid)
            {
                if (_unitOfWork.Customers == null)
                {
                    return Problem("Entity set 'DataContext.KhachHangs'  is null.");
                }
                var convertToKhachHang = _mapper.Map<KhachHang>(khachHang);
                convertToKhachHang.SoLanNghi = 1;
                convertToKhachHang.XepHang = false;
                await _unitOfWork.Customers.AddAsync(convertToKhachHang);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetKhachHang", new { id = convertToKhachHang.MaKh }, new Result()
                {
                    Success = true,
                    Data = khachHang
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

        // DELETE: api/KhachHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            if (_unitOfWork.Customers == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "entity Customer don't exist"
                    }
                });
            }
            var khachHang = await _unitOfWork.Customers.GetAsync(id);
            if (khachHang == null)
            {
                return NotFound(new Result()
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Customer don't exist"
                    }
                });
            }

            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
