using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminStaffServices
    {
        Task<ServerRespone> CreateNew(AdminStaff entity);
        Task<ServerRespone> Delete(object key);
        Task<IEnumerable<AdminStaff>> GetAll(int skip = 0, int take = 0, bool isForceRefresh = false);
        Task<AdminStaff> GetByID(object key);
        Task<ServerRespone> GetProfilePicture(int id);
        Task<ServerRespone> Update(AdminStaff entity);
        Task<ServerRespone> UploadProfilePicture(int id, string filePath);
    }
}