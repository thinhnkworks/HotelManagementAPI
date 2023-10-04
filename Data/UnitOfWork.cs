using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Core.Repositories;

namespace HotelManagementAPI.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        public ICustomerRepository Customers { get; set; }
        public UnitOfWork(DataContext dataContext, ILoggerFactory loggerFactory ) {
            _context = dataContext;
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
            Customers = new CustomerRepository(dataContext, _logger);
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
