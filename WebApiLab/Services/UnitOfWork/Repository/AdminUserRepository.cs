using CommonClass.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.UnitOfWork.Interface;

namespace WebApiLab.Services.UnitOfWork.Repository
{
    public class AdminUserRepository : GenericRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(LabDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override AdminUser GetById(object id)
        {
            var userName = new SqlParameter("ID", id);
            string query = @"SELECT TOP (1) *
                          FROM [LabDB].[dbo].[Admin.Users]
                          where UserName COLLATE SQL_Latin1_General_CP1_CS_AS = @ID";
            var user = Context.AdminUsers.FromSqlRaw(query, userName).FirstOrDefault();
            return user;
        }

    }
}
