using CommonClass.Models;

namespace WebApiLab.Services
{
    public class AdminUsersService : IBackendService<AdminUser>
    {
        public Task<bool> Create(AdminUser data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> Get(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUser>> Gets(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(AdminUser data)
        {
            throw new NotImplementedException();
        }
    }
}
