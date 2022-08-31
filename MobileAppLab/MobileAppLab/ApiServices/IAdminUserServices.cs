using CommonClass.Models;
using CommonClass.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IAdminUserServices
    {
        Task<ServerRespone> CreateNew(AdminUser entity);
        Task<ServerRespone> Delete(object key);
        Task<IEnumerable<AdminUser>> GetAll(int skip = 0, int take = 0, bool forceRefresh = false);
        Task<AdminUser> GetByID(object key);
        Task<ServerRespone> GetUserPicture(string userName);
        Task<(bool, string)> Login(string userName, string password);
        Task<ServerRespone> Update(AdminUser entity);
        Task<ServerRespone> UploadUserPicture(string userName, string filePath);
    }
}