using CommonClass.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    /// <summary>
    /// Lớp DAL bảng AdminUser
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="WebApiLab.Services.Interfaces.IAdminUsers&lt;CommonClass.Models.AdminUser&gt;" />
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
            var userName = new SqlParameter("ID", key);
            string query = @"SELECT TOP (1) [UserID]
                              ,[HashedPassword]
                              ,[Salt]
                              ,[IsResetPassword]
                              ,[DateModified]
                              ,[AccountStatus]
                              ,[ID]
                              ,[DateCreated]
                              ,[UserCreated]
                              ,[UserModified]
                          FROM [LabDB].[dbo].[Admin.Users]
                          where UserID COLLATE SQL_Latin1_General_CP1_CS_AS = @ID";
            var user = this._labDbContext.AdminUsers.FromSqlRaw(query, userName).FirstOrDefault();
            return Task.FromResult<AdminUser?>(user);
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
