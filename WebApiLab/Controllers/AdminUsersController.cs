using CommonClass.ErrorCodes;
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
        private AdminUsersService _adminUsersService;
        private AdminStaffsService _adminStaffsService;
        private JwtSettings _jwtSettings;
        public AdminUsersController(JwtSettings jwtSettings, IServiceProvider configuration)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings;
            this._adminUsersService = new AdminUsersService(configuration);
            this._adminStaffsService = new AdminStaffsService(configuration);
        }
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

                throw;
            }
        }
    }
}
