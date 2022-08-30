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
    public class AdminUserDAL :LabDbContext, IAdminUsers<AdminUser>
    {
        private ILogger<AdminUserDAL> _logger;
        public AdminUserDAL(DbContextOptions dbContextOptions, ILogger<AdminUserDAL> logger) : base(dbContextOptions)
        {
            this._logger = logger;
        }

        public async Task<bool> AddAsync(AdminUser user)
        {
            try
            {
                user.UserCreated = "System";
                user.UserModified = "System";
                user.DateCreated = DateTime.Now;
                user.DateModified = DateTime.Now;
                if (this.AdminUsers.AddIfNotExists(user, db => db.UserID == user.UserID) != null)
                {
                    await this.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminUserDAL));
                return false;
            }
            
        }

        public Task<bool> DeleteAsync(object key)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser?> Get(object key)
        {
            try
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
                var user = this.AdminUsers.FromSqlRaw(query, userName).FirstOrDefault();
                return Task.FromResult<AdminUser?>(user);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminUserDAL));
                return null;
            }

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
