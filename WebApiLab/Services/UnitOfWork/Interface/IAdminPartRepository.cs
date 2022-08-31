using CommonClass.Models;

namespace WebApiLab.Services.UnitOfWork.Interface
{
    public interface IAdminPartRepository : IGenericRepository<AdminParts>
    {
        Task<bool> IsExistPartID(int partID);
    }
}
