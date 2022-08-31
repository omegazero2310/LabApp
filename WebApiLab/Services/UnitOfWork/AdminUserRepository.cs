using CommonClass.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;

namespace WebApiLab.Services.UnitOfWork
{
    public class AdminUserRepository : GenericRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(LabDbContext context, ILogger<AdminUserRepository> logger) : base(context, logger)
        {
        }
        public override AdminUser GetById(object id)
        {
            var userName = new SqlParameter("ID", id);
            string query = @"SELECT TOP (1) *
                          FROM [LabDB].[dbo].[Admin.Users]
                          where UserName COLLATE SQL_Latin1_General_CP1_CS_AS = @ID";
            var user = this.Context.AdminUsers.FromSqlRaw(query, userName).FirstOrDefault();
            return user;
        }
    }
}
