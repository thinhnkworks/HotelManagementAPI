using HotelManagementAPI.Data;
using HotelManagementAPI.Models;
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
