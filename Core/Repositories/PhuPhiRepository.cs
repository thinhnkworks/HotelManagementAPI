using HotelManagementAPI.Core.IRepositories;
using HotelManagementAPI.Data;
using HotelManagementAPI.Helper;
using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.Core.Repositories
{
    public class PhuPhiRepository : GenericRepository<PhuPhi>, IPhuPhiRepository
    {

        public PhuPhiRepository(DataContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override async Task<bool> AddAsync(PhuPhi entity)
        {
            try
            {
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(PhuPhiRepository));
                return false;
            }
        }
        public override async Task<IEnumerable<PhuPhi>> GetAllAsync()
        {
            try
            {
                var phuPhis = await base.GetAllAsync();
                return phuPhis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GettAll method error", typeof(PhuPhiRepository));
                return new List<PhuPhi>();
            }
        }
        public override async Task<PhuPhi?> GetAsync(int id)
        {
            try
            {
                PhuPhi? phuPhi = await base.GetAsync(id);
                if (phuPhi == null)
                    return null;
                return phuPhi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get method error", typeof(PhuPhiRepository));
                return null;
            }
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var phuPhiExist = await _dbSet.FindAsync(id);

                if (phuPhiExist == null)
                    return false;
                _dbSet.Remove(phuPhiExist);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} delete method error", typeof(PhuPhiRepository));
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(int id, PhuPhi entity)
        {
            try
            {
                var phuPhiExist = await _dbSet.FirstOrDefaultAsync(x => x.MaPp == id);

                if (phuPhiExist == null)
                {
                    return await AddAsync(entity);
                }
                if (!String.IsNullOrEmpty(entity.TenPp))
                {
                    phuPhiExist.TenPp = entity.TenPp;
                }
                if (entity.Gia >= 0)
                {
                    phuPhiExist.Gia = entity.Gia;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} update method error", typeof(PhuPhiRepository));
                return false;
            }
        }
    }
}
