using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using Newtonsoft.Json;
using System.Net;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.DataAccessLayer;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.BusinessLayer
{

    /// <summary>CRUD trên bảng Admin.Users</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminUsersService
    {
        private IAdminUsers<AdminUser> _adminUsers;
        private IAdminStaffs<AdminStaff> _adminStaff;
        private JwtSettings _jwtSettings;
        public AdminUsersService(IServiceProvider serviceProvider, JwtSettings jwtSettings)
        {
            _adminUsers = new AdminUserDAL(serviceProvider);
            _adminStaff = new AdminStaffDAL(serviceProvider);
            this._jwtSettings = jwtSettings;

        }
        public async Task<HttpResponseMessage> Create(CreateAccountRequest data)
        {
            var account = await this._adminUsers.Get(data.UserID);
            if (account == null)
            {

                AdminStaff newStaff = new AdminStaff();
                newStaff.UserName = data.UserName;
                newStaff.Address = data.Address;
                newStaff.Email = data.Email;
                newStaff.PartID = 1;
                newStaff.PhoneNumber = data.PhoneNumber;
                newStaff.Gender = data.Gender;
                var salt = PasswordHelper.CreateSalt();
                var hashedPassword = PasswordHelper.GenerateHash(data.Password, salt);
                AdminUser newUser = new AdminUser { Staff = newStaff };
                newUser.UserID = data.UserID;
                newUser.HashedPassword = hashedPassword;
                newUser.Salt = salt;
                newUser.AccountStatus = CommonClass.Enums.AccountStatusOptions.Normal;
                newUser.IsResetPassword = false;
                newUser.DateModified = DateTime.Now;
                await this._adminStaff.AddAsync(newStaff, "System");
                newUser.ID = newStaff.ID;
                await this._adminUsers.AddAsync(newUser);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
        public async Task<UserTokens> CheckLogin(string userName, string password)
        {
            var token = new UserTokens();
            var user = await this._adminUsers.Get(userName);
            if (user == null)
            {
                throw new Exception(UserLoginErrorCode.NOT_EXIST);
            }
            else
            {
                bool checkPassword = PasswordHelper.AreEqual(password, user.HashedPassword, user.Salt);
                if (checkPassword)
                {
                    if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Suppended)
                    {
                        throw new Exception(UserLoginErrorCode.SUPPENDED);
                    }
                    else if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Banned)
                    {
                        throw new Exception(UserLoginErrorCode.BANNED);
                    }
                    else
                    {
                        var adminUser = await this._adminUsers.Get(userName);
                        var userInfo = await this._adminStaff.Get(adminUser?.ID ?? -1);
                        if (userInfo != null)
                        {
                            token = JwtHelpers.GenTokenkey(new UserTokens()
                            {
                                EmailId = userInfo.Email,
                                GuidId = Guid.NewGuid(),
                                UserName = userName,
                                Id = userInfo.ID
                            }, _jwtSettings);
                        }
                        else
                        {
                            token = JwtHelpers.GenTokenkey(new UserTokens()
                            {
                                EmailId = "",
                                GuidId = Guid.NewGuid(),
                                UserName = userName,
                            }, _jwtSettings);
                        }
                        return token;
                    }
                }
                else
                {
                    throw new Exception(UserLoginErrorCode.WRONG_USER_NAME_PASSWORD);
                }
            }
        }

    }
}
