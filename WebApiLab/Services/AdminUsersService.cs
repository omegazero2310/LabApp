using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;

namespace WebApiLab.Services
{

    /// <summary>CRUD trên bảng Admin.Users</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminUsersService : IBackendService<AdminUser>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        public AdminUsersService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
        }
        public Task<bool> Create(AdminUser data)
        {
            if (this._labDbContext?.AdminUsers.AddIfNotExists(data, db => db.UserID == data.UserID) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
            
        }

        public Task<bool> Delete(object key)
        {
            if (this._labDbContext?.AdminUsers.DeleteIfExists(new AdminUser { UserID = key.ToString()}, db => db.UserID == key.ToString()) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }

        public Task<AdminUser?> Get(object key)
        {
            return Task.FromResult<AdminUser?>(this._labDbContext?.AdminUsers.Where(user => user.UserID == key.ToString()).FirstOrDefault());
        }

        public Task<IEnumerable<AdminUser>> Gets(int skip, int take)
        {
            throw new NotImplementedException("Not support");
        }

        public Task<bool> Update(AdminUser data)
        {
            if (this._labDbContext?.AdminUsers.UpdateIfExists(data, db => db.UserID == data.UserID.ToString()) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
    }
}
