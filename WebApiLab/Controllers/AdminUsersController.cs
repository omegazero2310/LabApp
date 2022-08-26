using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Mvc;
using WebApiLab.Exts;
using WebApiLab.Services;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly IServiceProvider _configuration;
        private AdminUsersService _adminUsersService;
        private JwtSettings _jwtSettings;
        private ILogger<AdminUsersController> _logger;
        public AdminUsersController(JwtSettings jwtSettings, IServiceProvider configuration, ILogger<AdminUsersController> logger)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings;
            this._logger = logger;
            this._adminUsersService = new AdminUsersService(configuration, jwtSettings);
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
        public async Task<IActionResult> GetToken([FromBody] LoginRequest userLogins)
        {
            try
            {
                return Ok(await this._adminUsersService.CheckLogin(userLogins.UserName, userLogins.Password));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                if (ex.Message == UserLoginErrorCode.NOT_EXIST)
                    return NotFound(UserLoginErrorCode.NOT_EXIST);
                if (ex.Message == UserLoginErrorCode.SUPPENDED)
                    return Unauthorized(UserLoginErrorCode.SUPPENDED);
                if (ex.Message == UserLoginErrorCode.BANNED)
                    return Unauthorized(UserLoginErrorCode.BANNED);
                if (ex.Message == UserLoginErrorCode.WRONG_USER_NAME_PASSWORD)
                    return Unauthorized(UserLoginErrorCode.WRONG_USER_NAME_PASSWORD);
                else
                    throw ex;
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
        public async Task<HttpResponseMessage> CreateNewAccount([FromBody] CreateAccountRequest createAccRequet)
        {
            try
            {
                return await this._adminUsersService.Create(createAccRequet);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
