using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    public class AdminUserDAL : IAdminUsers<AdminUser>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        public AdminUserDAL(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();

        }
        public async Task<bool> AddAsync(AdminUser user)
        {
            user.UserCreated = "System";
            user.UserModified = "System";
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            if (this._labDbContext?.AdminUsers.AddIfNotExists(user, db => db.UserID == user.UserID) != null)
            {
                await this._labDbContext?.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public Task<bool> DeleteAsync(object key)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser?> Get(object key)
        {
            return Task.FromResult<AdminUser?>(this._labDbContext?.AdminUsers.Where(user => user.UserID == key.ToString()).FirstOrDefault());
        }

        public Task<IEnumerable<AdminUser>> Gets(int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AdminUser user, string _userName)
        {
            throw new NotImplementedException();
        }
    }
}
