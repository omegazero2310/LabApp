using CommonClass.Models.Request;

namespace WebApiLab.Services.BusinessLayer
{
    /// <summary>
    /// Interface bảng AdminUser
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    public interface IAdminUsersService
    {
        Task<ServerRespone> CheckLogin(string userName, string password);
        Task<ServerRespone> Create(CreateAccountRequest data);
        Task<ServerRespone> GetUserInfo(string userName, string rootPath = "");
        Task<ServerRespone> GetUserPicture(string userName, string rootPath);
        Task<ServerRespone> UploadUserPicture(string userName, string rootPath, IFormFile file);
    }
}