using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NhanVienController : ControllerBase
	{
		private readonly DataContext _dataContext;
		public NhanVienController(DataContext dataContext)
		{
			this._dataContext = dataContext;
		}
		[HttpGet]
		public IEnumerable<NhanVien> Get()
		{
			return _dataContext.NhanViens.ToList();
		}
	}
}
