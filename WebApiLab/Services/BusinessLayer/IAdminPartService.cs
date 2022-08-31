using CommonClass.Models;
using CommonClass.Models.Request;

namespace WebApiLab.Services.BusinessLayer
{
    /// <summary>
    /// Interface của bảng AdminParts
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    public interface IAdminPartService
    {
        Task<ServerRespone> Create(AdminParts data);
        Task<ServerRespone> Delete(object key);
        Task<ServerRespone> Get(object key);
        Task<ServerRespone> Gets(int skip, int take);
        Task<ServerRespone> Update(AdminParts data);
    }
}