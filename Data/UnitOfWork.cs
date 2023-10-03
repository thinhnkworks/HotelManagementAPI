using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Core.Repositories;
using HotelManagementAPI.Helper;

namespace HotelManagementAPI.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public ICustomerRepository Customers { get; set; }
        private readonly IHelper _helper;
        public INhanVienRepository NhanViens { get; set; }
        public UnitOfWork(DataContext dataContext, ILoggerFactory loggerFactory,IHelper helper, IConfiguration configuration)
        {
            _context = dataContext;
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
            _helper = helper;
            _configuration = configuration;
            Customers = new CustomerRepository(dataContext, _logger);
            NhanViens = new NhanVienRepository(dataContext, _logger, _helper, _configuration);
        }
        public async Task CompleteAsync()
        {
            try {
                await _context.SaveChangesAsync();
            }  catch (Exception ex) {
                _logger.LogError(ex, "{Repo} SaveChange method error", typeof(UnitOfWork));
                throw;
            }                
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
