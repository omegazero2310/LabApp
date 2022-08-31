using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiLab.Exts;
using WebApiLab.Services;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    /// <summary>
    /// API Controller tương tác với bảng Admin.Users và lấy JWT token
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private IAdminUsersService _adminUsersService;
        private ILogger<AdminUsersController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminUsersController(IAdminUsersService adminUsersService, ILogger<AdminUsersController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._adminUsersService = adminUsersService;
            this._hostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="userLogins">json object chứa thông tin đăng nhập.</param>
        /// <returns>JWT token để đăng nhập</returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        /// <response code="200">Đăng nhập thành công, trả về token</response>
        /// <response code="401">1. sai tên đăng nhập hoặc mật khẩu ; 2. tài khoản bị vô hiệu hóa</response>
        /// <response code="404">Tài khoản không tồn tại</response>
        [HttpPost]
        [ActionName("Login")]
        public async Task<ServerRespone> GetToken([FromBody] LoginRequest userLogins)
        {
            try
            {
                return await this._adminUsersService.CheckLogin(userLogins.UserName, userLogins.Password);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get token failed");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }
        /// <summary>
        /// Tạo tài khoản
        /// </summary>
        /// <param name="userLogins">json object chứa thông tin đăng kí tài khoản.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        /// <response code="200">Đã tạo tài khoản</response>
        /// <response code="401">Đã tồn tại tài khoản</response>
        [HttpPost]
        [ActionName("CreateAccount")]
        public async Task<ServerRespone> CreateNewAccount([FromBody] CreateAccountRequest createAccRequet)
        {
            try
            {
                return await this._adminUsersService.Create(createAccRequet);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get token failed");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }
        [HttpPost]
        [ActionName("UploadProfilePicture")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ServerRespone> UploadPicture([FromQuery] string userName, IFormFile file)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_hostEnvironment.WebRootPath))
                {
                    _hostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                return await this._adminUsersService.UploadUserPicture(userName, _hostEnvironment.WebRootPath, file);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to get Profile Picture");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }
        [HttpGet]
        [ActionName("GetProfilePicture")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ServerRespone> GetPicture([FromQuery] string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_hostEnvironment.WebRootPath))
                {
                    _hostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var data = await this._adminUsersService.GetUserPicture(userName, _hostEnvironment.WebRootPath);
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to get Profile Picture");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }

        }
        [HttpGet]
        [ActionName("Get")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ServerRespone> GetInfo([FromQuery] string userName)
        {
            try
            {
                
                var data = await this._adminUsersService.GetUserInfo(userName);
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to get UserInfo");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }

        }

    }
}
