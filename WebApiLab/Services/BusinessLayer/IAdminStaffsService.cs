using CommonClass.Models;
using CommonClass.Models.Request;

namespace WebApiLab.Services.BusinessLayer
{
    public interface IAdminStaffsService
    {
        Task<ServerRespone> Create(AdminStaff data);
        Task<ServerRespone> Delete(object key);
        Task<ServerRespone> Get(object key);
        Task<ServerRespone> GetProfilePicture(int id, string rootPath);
        Task<ServerRespone> Gets(int skip, int take);
        Task<ServerRespone> Update(AdminStaff data);
        Task<ServerRespone> UpdateProfilePicture(int id, string rootPath, IFormFile file);
    }
}