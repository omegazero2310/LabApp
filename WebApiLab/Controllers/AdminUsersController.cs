using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Mvc;
using WebApiLab.Exts;
using WebApiLab.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly IServiceProvider _configuration;
        private IBackendService<AdminUser> _adminUsersService;
        private IBackendService<AdminStaff> _adminStaffsService;
        private JwtSettings _jwtSettings;
        private ILogger<AdminUsersController> _logger;
        public AdminUsersController(JwtSettings jwtSettings, IServiceProvider configuration, ILogger<AdminUsersController> logger)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings;
            this._logger = logger;
            this._adminUsersService = new AdminUsersService(configuration);
            this._adminStaffsService = new AdminStaffsService(configuration);
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
                var token = new UserTokens();
                var user = await _adminUsersService.Get(userLogins.UserName);
                if (user == null)
                    return NotFound(UserLoginErrorCode.NOT_EXIST);
                else
                {
                    bool checkPassword = PasswordHelper.AreEqual(userLogins.Password, user.HashedPassword, user.Salt);
                    if (checkPassword)
                    {
                        if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Suppended)
                            return Unauthorized(UserLoginErrorCode.SUPPENDED);
                        else if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Banned)
                            return Unauthorized(UserLoginErrorCode.BANNED);
                        else
                        {
                            var userInfo = await this._adminStaffsService.Get(userLogins.UserName);
                            if (userInfo != null)
                            {
                                token = JwtHelpers.GenTokenkey(new UserTokens()
                                {
                                    EmailId = userInfo.Email,
                                    GuidId = Guid.NewGuid(),
                                    UserName = userLogins.UserName,
                                }, _jwtSettings);
                            }  
                            else
                            {
                                token = JwtHelpers.GenTokenkey(new UserTokens()
                                {
                                    EmailId = "",
                                    GuidId = Guid.NewGuid(),
                                    UserName = userLogins.UserName,
                                }, _jwtSettings);
                            }    
                            return Ok(token);
                        }
                    }
                    else
                        return Unauthorized(UserLoginErrorCode.WRONG_USER_NAME_PASSWORD);
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ActionName("CreateAccount")]
        public async Task<IActionResult> CreateNewAccount([FromBody] CreateAccountRequest createAccRequet)
        {
            try
            {
                var account = await this._adminUsersService.Get(createAccRequet.UserName);
                if (account == null)
                {

                    AdminStaff newStaff = new AdminStaff();
                    newStaff.UserName = createAccRequet.UserName;
                    newStaff.Address = createAccRequet.Address;
                    newStaff.Email = createAccRequet.Email;
                    newStaff.PositionID = CommonClass.Enums.PositionOptions.NV;
                    newStaff.PhoneNumber = createAccRequet.PhoneNumber;
                    newStaff.Gender = createAccRequet.Gender;
                    var salt = PasswordHelper.CreateSalt();
                    var hashedPassword = PasswordHelper.GenerateHash(createAccRequet.Password, salt);
                    AdminUser newUser = new AdminUser { Staff = newStaff };
                    newUser.UserID = createAccRequet.UserID;
                    newUser.HashedPassword = hashedPassword;
                    newUser.Salt = salt;
                    newUser.AccountStatus = CommonClass.Enums.AccountStatusOptions.Normal;
                    newUser.IsResetPassword = false;
                    newUser.DateModified = DateTime.Now;
                    await this._adminStaffsService.Create(newStaff);
                    newUser.ID = newStaff.ID;
                    await this._adminUsersService.Create(newUser);
                    return Ok();
                }
                else
                    return Unauthorized(UserLoginErrorCode.EXIST);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }
    }
}
