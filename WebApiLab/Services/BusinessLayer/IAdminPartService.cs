using CommonClass.Models;
using CommonClass.Models.Request;

namespace WebApiLab.Services.BusinessLayer
{
    public interface IAdminPartService
    {
        Task<ServerRespone> Create(AdminParts data);
        Task<ServerRespone> Delete(object key);
        Task<ServerRespone> Get(object key);
        Task<ServerRespone> Gets(int skip, int take);
        Task<ServerRespone> Update(AdminParts data);
    }
}