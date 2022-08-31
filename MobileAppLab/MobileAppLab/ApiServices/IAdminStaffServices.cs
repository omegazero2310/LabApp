using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminStaffServices : IService<AdminStaff>
    {
        Task<ServerRespone> GetProfilePicture(int id);
        Task<ServerRespone> UploadProfilePicture(int id, string filePath);
    }
}