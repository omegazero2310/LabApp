using CommonClass.Models.Request;

namespace WebApiLab.Services.BusinessLayer
{
    public interface IAdminUsersService
    {
        Task<ServerRespone> CheckLogin(string userName, string password);
        Task<ServerRespone> Create(CreateAccountRequest data);
    }
}