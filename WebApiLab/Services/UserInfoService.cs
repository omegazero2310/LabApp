using CommonClass.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiLab.LabDatabaseContext;

namespace WebApiLab.Services
{
    public class UserInfoService : IBackendService<UserInfo>
    {
        private IServiceProvider _serviceProvider;
        private LabDbContext _labDbContext;
        public UserInfoService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = _serviceProvider.GetService<LabDbContext>();
        }
        public async Task<bool> Create(UserInfo data)
        {
            if (!this._labDbContext.UserInfo.Any(s => s.UserName == data.UserName))
            {
                await this._labDbContext.UserInfo.AddAsync(data);
                await this._labDbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> Delete(object key)
        {
            if (this._labDbContext.UserInfo.Any(s => s.UserName == key.ToString()))
            {
                this._labDbContext.UserInfo.Remove(new UserInfo { UserName = key.ToString() });
                await this._labDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public Task<UserInfo> Get(object key)
        {
            return this._labDbContext.UserInfo.Where(s => s.UserName == key.ToString()).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<UserInfo>> Gets(int skip = 0, int take = 0)
        {
            if (skip > 0 && take > 0)
                return Task.FromResult<IEnumerable<UserInfo>>(this._labDbContext.UserInfo.Skip(skip).Take(take).ToList()) ;
            else if (skip > 0)
                return Task.FromResult<IEnumerable<UserInfo>>(this._labDbContext.UserInfo.Skip(skip).ToList());
            else if (take > 0)
                return Task.FromResult<IEnumerable<UserInfo>>(this._labDbContext.UserInfo.Take(take).ToList());
            else
                return Task.FromResult<IEnumerable<UserInfo>>(this._labDbContext.UserInfo.ToList());
        }

        public async Task<bool> Update(UserInfo data)
        {
            if (this._labDbContext.UserInfo.Any(s => s.UserName == data.UserName))
            {
                this._labDbContext.UserInfo.Update(data);
                await this._labDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
