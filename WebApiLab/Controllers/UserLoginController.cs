using Microsoft.AspNetCore.Mvc;
using WebApiLab.Exts;
using WebApiLab.LabDatabaseContext;
using WebApiLab.Models;

namespace WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        private IServiceProvider _serviceProvider;
        private LabDbContext _labDbContext;
        public UserLoginController(JwtSettings jwtSettings, IServiceProvider serviceProvider)
        {
            this._jwtSettings = jwtSettings;
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
        }
        [HttpPost]
        public IActionResult GetToken([FromBody] LoginRequest userLogin)
        {
            try
            {
                var Token = new UserTokens();
                //TODO: compare password base on hash, not plain text
                var Valid = _labDbContext.UserLogin.Any(x => x.UserName.Equals(userLogin.UserName) && PasswordHelper.ComparePassowrd(userLogin.Password,x.HashedPassword,x.Salt));
                if (Valid)
                {
                    var user = _labDbContext.UserInfo.FirstOrDefault(x => x.UserName.Equals(userLogin.UserName));
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.Email,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong user name or password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IActionResult RegisterAccount([FromBody] string loginForm)
        {
            return Ok();
        }
    }
}
