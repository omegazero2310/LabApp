using CommonClass.Models;
using WebApiLab.DatabaseContext;

namespace WebApiLab.Services.UnitOfWork
{
    public class AdminPartRepository : GenericRepository<AdminParts>, IAdminPartRepository
    {
        public AdminPartRepository(LabDbContext context, ILogger<AdminPartRepository> logger) : base(context, logger)
        {
        }
    }
}
