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

        public Task<bool> IsExistPartID(int partID)
        {
            return Task.FromResult(this.Context.AdminParts.Any(part => part.PartID == partID));
        }
    }
}
