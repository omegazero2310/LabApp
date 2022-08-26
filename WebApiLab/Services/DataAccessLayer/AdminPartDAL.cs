using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    public class AdminPartDAL : IAdminParts<AdminParts>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        public AdminPartDAL(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
        }
        public Task<bool> AddAsync(AdminParts user, string userAdd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object key)
        {
            throw new NotImplementedException();
        }

        public Task<AdminParts?> Get(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdminParts>> Gets(int skip = 0, int take = 0)
        {
            if (skip > 0 && take > 0)
                return this._labDbContext?.AdminParts.Skip(skip).Take(take).ToList() ?? new List<AdminParts>();
            else if (take > 0)
                return this._labDbContext?.AdminParts.Take(take).ToList() ?? new List<AdminParts>();
            else if (skip > 0)
                return this._labDbContext?.AdminParts.Skip(skip).ToList() ?? new List<AdminParts>();
            else
                return this._labDbContext?.AdminParts.ToList() ?? new List<AdminParts>();
        }

        public Task<bool> UpdateAsync(AdminParts user, string userUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
