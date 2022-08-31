using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.UnitOfWork.Interface;

namespace WebApiLab.Services.UnitOfWork.Repository
{
    /// <summary>
    /// Lớp chứa các tao tác dữ liệu trên bảng AdminPart
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    /// <seealso cref="WebApiLab.Services.UnitOfWork.Repository.GenericRepository&lt;CommonClass.Models.AdminParts&gt;" />
    /// <seealso cref="WebApiLab.Services.UnitOfWork.Interface.IAdminPartRepository" />
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
