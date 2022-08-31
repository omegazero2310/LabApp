using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.UnitOfWork.Interface;

namespace WebApiLab.Services.UnitOfWork.Repository
{
    public class AdminPartRepository : GenericRepository<AdminParts>, IAdminPartRepository
    {
        public AdminPartRepository(LabDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
