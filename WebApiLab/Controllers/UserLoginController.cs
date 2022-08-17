using CommonClass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApiLab.Exts;
using WebApiLab.LabDatabaseContext;

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [ActionName("GetToken")]
        public IActionResult GetToken([FromBody] LoginRequest userLogin)
        {
            try
            {
                var token = new UserTokens();
                var userFind = _labDbContext.UserLogin.Where(x => x.UserName.Equals(userLogin.UserName)).FirstOrDefault();
                if (userFind == null)
                    return BadRequest($"wrong user name or password");
                var Valid = PasswordHelper.AreEqual(userLogin.Password, userFind.HashedPassword, userFind.Salt);
                if (Valid)
                {
                    var user = _labDbContext.UserInfo.FirstOrDefault(x => x.UserName.Equals(userLogin.UserName));
                    token = JwtHelpers.GenTokenkey(new UserTokens()
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
                return Ok(token);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [ActionName("Register")]
        public IActionResult RegisterAccount([FromBody] CreateAccountRequest createForm)
        {
            if (createForm == null)
                return BadRequest();
            else
            {
                if (string.IsNullOrEmpty(createForm.Email) && string.IsNullOrEmpty(createForm.PhoneNumber))
                    return BadRequest("Email or Phone Number Required");
                else
                {
                    bool isExistEmail = this._labDbContext.UserInfo.Any(x => x.Email.Equals(createForm.Email));
                    bool isExistPhoneNumber = this._labDbContext.UserInfo.Any(x => x.PhoneNumber.Equals(createForm.PhoneNumber));
                    bool isExistUserName = this._labDbContext.UserInfo.Any(x => x.UserName.Equals(createForm.UserName)) 
                        || this._labDbContext.UserLogin.Any(x => x.UserName.Equals(createForm.UserName));
                    bool isValidEmail = (new System.ComponentModel.DataAnnotations.EmailAddressAttribute()).IsValid(createForm.Email ?? "passed@mail.com");
                    bool isVaildPhoneNumer = createForm.PhoneNumber?.Trim('+').Replace(" ", "").Replace("-","").IsDigitOnlyString() ?? true;
                    if (isExistEmail)
                        return BadRequest("Email Existed");
                    if (isExistPhoneNumber)
                        return BadRequest("Phone Number Existed");
                    if (isExistUserName)
                        return BadRequest("UserName Existed");
                    if (!isValidEmail)
                        return BadRequest("Email Invalid");
                    if (!isVaildPhoneNumer)
                        return BadRequest("Phone Number Invalid");

                    var userInfo = new UserInfo();
                    userInfo.UserName = createForm.UserName;
                    userInfo.FirstName = createForm.FirstName;
                    userInfo.LastName = createForm.LastName;
                    userInfo.PhoneNumber = createForm.PhoneNumber ?? "";
                    userInfo.Email = createForm.Email ?? "";
                    userInfo.CreatedDate = DateTime.Now;
                    userInfo.Gender = createForm.Gender;
                    userInfo.Status = CommonClass.Enums.AccountStatusOption.Using;
                    userInfo.IsNew = true;
                    userInfo.TotalTrips = 0;
                    var userLogin = new UserLogin();
                    userLogin.UserName = createForm.UserName;
                    var salt = PasswordHelper.CreateSalt();
                    var hashedPass = PasswordHelper.GenerateHash(createForm.Password, salt);
                    userLogin.HashedPassword = hashedPass;
                    userLogin.Salt = salt;
                    userLogin.DateModified = DateTime.Now;
                    userLogin.IsResetPassword = false;
                    this._labDbContext.UserInfo.Add(userInfo);
                    this._labDbContext.UserLogin.Add(userLogin);
                    this._labDbContext.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
