using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminUserServices:IService<AdminUser>
    {
        Task<AdminUser> GetByID(object key, string token);
        Task<ServerRespone> GetUserPicture(string userName, string token);
        Task<(bool, string)> Login(string userName, string password);
        Task<ServerRespone> UploadUserPicture(string userName, string filePath, string token);
    }
}