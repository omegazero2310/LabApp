using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using Newtonsoft.Json;
using System.Net;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.UnitOfWork;

namespace WebApiLab.Services.BusinessLayer
{

    /// <summary>Lớp nghiệp vụ tương tác với bảng AdminUsers</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private JwtSettings _jwtSettings;
        private ILogger<AdminUsersService> _logger;
        public AdminUsersService(IUnitOfWork unitOfWork, JwtSettings jwtSettings, ILogger<AdminUsersService> logger)
        {
            _unitOfWork = unitOfWork;
            this._jwtSettings = jwtSettings;
            _logger = logger;
        }
        public async Task<ServerRespone> Create(CreateAccountRequest data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var account = this._unitOfWork.AdminUserRepository.Find(user => user.UserName == data.UserName);
                if (account.Count() == 0)
                {
                    var salt = PasswordHelper.CreateSalt();
                    var hashedPassword = PasswordHelper.GenerateHash(data.Password, salt);
                    AdminUser newUser = new AdminUser();
                    newUser.Id = Guid.NewGuid();
                    newUser.UserName = data.UserName;
                    newUser.HashedPassword = hashedPassword;
                    newUser.Salt = salt;
                    newUser.AccountStatus = CommonClass.Enums.AccountStatusOptions.Normal;
                    newUser.IsResetPassword = false;
                    newUser.DateModified = DateTime.Now;
                    newUser.DisplayName = data.Name;
                    newUser.PhoneNumber = data.PhoneNumber;
                    newUser.UserCreated = "CreateRequestAPI";
                    newUser.UserModified = "CreateRequestAPI";
                    this._unitOfWork.AdminUserRepository.Add(newUser, "RegisterSystem");
                    _unitOfWork.Save();
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Created";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = "Existed";
                    serverRespone.HttpStatusCode = HttpStatusCode.Forbidden;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "CreateNewUserError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }
        public async Task<ServerRespone> CheckLogin(string userName, string password)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var token = new UserTokens();
                var user = this._unitOfWork.AdminUserRepository.GetById(userName);
                if (user == null)
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = UserLoginErrorCode.NOT_EXIST;
                    serverRespone.HttpStatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    bool checkPassword = PasswordHelper.AreEqual(password, user.HashedPassword, user.Salt);
                    if (checkPassword)
                    {
                        if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Suppended)
                        {
                            serverRespone.IsSuccess = false;
                            serverRespone.Message = UserLoginErrorCode.SUPPENDED;
                            serverRespone.HttpStatusCode = HttpStatusCode.Forbidden;
                        }
                        else if (user.AccountStatus == CommonClass.Enums.AccountStatusOptions.Banned)
                        {
                            serverRespone.IsSuccess = false;
                            serverRespone.Message = UserLoginErrorCode.BANNED;
                            serverRespone.HttpStatusCode = HttpStatusCode.Forbidden;
                        }
                        else
                        {
                            token = JwtHelpers.GenTokenkey(new UserTokens()
                            {
                                Id = user.Id,
                                GuidId = Guid.NewGuid(),
                                UserName = userName,
                            }, _jwtSettings);
                            serverRespone.IsSuccess = true;
                            serverRespone.Message = "GetTokenSuccess";
                            serverRespone.HttpStatusCode = HttpStatusCode.OK;
                            serverRespone.Result = token;
                        }
                    }
                    else
                    {
                        serverRespone.IsSuccess = false;
                        serverRespone.Message = UserLoginErrorCode.WRONG_USER_NAME_PASSWORD;
                        serverRespone.HttpStatusCode = HttpStatusCode.Forbidden;
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "GetTokenError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }

    }
}
