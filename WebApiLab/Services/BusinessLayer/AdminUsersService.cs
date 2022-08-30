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

    /// <summary>Lớp nghiệp vụ tương tác với bảng AdminUsers</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminUsersService : IAdminUsersService
    {
        private IAdminUsers<AdminUser> _adminUsers;
        private IAdminStaffs<AdminStaff> _adminStaff;
        private JwtSettings _jwtSettings;
        private ILogger<AdminUsersService> _logger;
        public AdminUsersService(IAdminStaffs<AdminStaff> adminStaffsDAL, IAdminUsers<AdminUser> adminUsers, JwtSettings jwtSettings, ILogger<AdminUsersService> logger)
        {
            _adminUsers = adminUsers;
            _adminStaff = adminStaffsDAL;
            this._jwtSettings = jwtSettings;
            _logger = logger;
        }
        public async Task<ServerRespone> Create(CreateAccountRequest data)
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
                return new ServerRespone { IsSuccess = true, Message = "Created", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = false, Message = "Existed", HttpStatusCode = HttpStatusCode.Forbidden, Result = null };
        }
        public async Task<ServerRespone> CheckLogin(string userName, string password)
        {
            var token = new UserTokens();
            var user = await this._adminUsers.Get(userName);
            if (user == null)
            {
                return new ServerRespone { IsSuccess = false, Message = UserLoginErrorCode.NOT_EXIST, HttpStatusCode = HttpStatusCode.NotFound, Result = null };
            }
            else
            {
                bool checkPassword = PasswordHelper.AreEqual(password, user.HashedPassword, user.Salt);
                if (checkPassword)
                {
                    if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Suppended)
                    {
                        return new ServerRespone { IsSuccess = false, Message = UserLoginErrorCode.SUPPENDED, HttpStatusCode = HttpStatusCode.Forbidden, Result = null };
                    }
                    else if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Banned)
                    {
                        return new ServerRespone { IsSuccess = false, Message = UserLoginErrorCode.BANNED, HttpStatusCode = HttpStatusCode.Forbidden, Result = null };
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
                        return new ServerRespone { IsSuccess = true, Message = "GetSuccess", HttpStatusCode = HttpStatusCode.OK, Result = token };

                    }
                }
                else
                {
                    return new ServerRespone { IsSuccess = false, Message = UserLoginErrorCode.WRONG_USER_NAME_PASSWORD, HttpStatusCode = HttpStatusCode.Forbidden, Result = null };
                }
            }
        }

    }
}
