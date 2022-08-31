using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using CommonClass.Validations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.UnitOfWork;

namespace WebApiLab.Services.BusinessLayer
{

    /// <summary>Lớp nghiệp vụ tương tác với bảng AdminStaff</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminStaffsService
    {
        private IUnitOfWork _unitOfWork;
        private AdminStaffValidator _validationRules;
        private string _userName;
        private readonly string _imageFolder = "ProfileImgs";
        private ILogger<AdminStaffsService> _logger;
        public AdminStaffsService(IUnitOfWork unitOfWork, IHttpContextAccessor currentContext, ILogger<AdminStaffsService> logger)
        {
            this._unitOfWork = unitOfWork;
            _validationRules = new AdminStaffValidator();
            _userName = currentContext.HttpContext.User.Identity.Name ?? "Unknows";
            _logger = logger;
        }
        public async Task<ServerRespone> Create(AdminStaff data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var result = _validationRules.Validate(data);
                if (!result.IsValid)
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = "DataValidateError";
                    serverRespone.HttpStatusCode = HttpStatusCode.BadRequest;
                    serverRespone.Result = result.Errors;
                }
                bool isDuplicateEmail = await _unitOfWork.AdminStaffRepository.IsDuplicateEmail(data.Email);
                bool isDuplicatePhoneNumber = await _unitOfWork.AdminStaffRepository.IsDuplicatePhoneNumber(data.PhoneNumber);
                if (isDuplicateEmail)
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = AdminStaffErrorCode.DUPLICATE_EMAIL;
                    serverRespone.HttpStatusCode = HttpStatusCode.BadRequest;
                }
                if (isDuplicatePhoneNumber)
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER;
                    serverRespone.HttpStatusCode = HttpStatusCode.BadRequest;
                }
                if (_unitOfWork.AdminStaffRepository.Add(data))
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Created";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                    _unitOfWork.Save();
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: Create New Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;


        }

        public async Task<ServerRespone> Delete(object key)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (_unitOfWork.AdminStaffRepository.Remove(new AdminStaff { StaffID = (int)key }))
                {
                    _unitOfWork.Save();
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Deleted";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: Delete Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }

        public async Task<ServerRespone> Get(object key)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var value = _unitOfWork.AdminStaffRepository.GetById(key);
                serverRespone.IsSuccess = true;
                serverRespone.Message = "GetSuccess";
                serverRespone.HttpStatusCode = HttpStatusCode.OK;
                serverRespone.Result = value;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: Get Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;
        }

        public async Task<ServerRespone> Gets(int skip, int take)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var value = _unitOfWork.AdminStaffRepository.GetAll();
                serverRespone.IsSuccess = true;
                serverRespone.Message = "GetsSuccess";
                serverRespone.HttpStatusCode = HttpStatusCode.OK;
                serverRespone.Result = value;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: Gets Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }

        public async Task<ServerRespone> Update(AdminStaff data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (await _unitOfWork.AdminStaffRepository.IsDuplicateEmail(data.Email, data.StaffID))
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = AdminStaffErrorCode.DUPLICATE_EMAIL;
                    serverRespone.HttpStatusCode = HttpStatusCode.BadRequest;
                }
                if (await _unitOfWork.AdminStaffRepository.IsDuplicatePhoneNumber(data.PhoneNumber, data.StaffID))
                {
                    serverRespone.IsSuccess = false;
                    serverRespone.Message = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER;
                    serverRespone.HttpStatusCode = HttpStatusCode.BadRequest;
                }
                if (_unitOfWork.AdminStaffRepository.Update(data))
                {
                    _unitOfWork.Save();
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Updated";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }    
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: Gets Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }
        public async Task<ServerRespone> UpdateProfilePicture(int id, string rootPath, IFormFile file)
        {
            ServerRespone serverRespone = new ServerRespone();
            serverRespone.IsSuccess = true;
            serverRespone.Message = "NoChange";
            serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
            try
            {
                string folderPath = Path.Combine(rootPath, _imageFolder);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                if (file.Length > 0)
                {
                    var user = _unitOfWork.AdminStaffRepository.GetById(id);
                    if (user != null)
                    {
                        if (string.IsNullOrEmpty(user.ProfileImage))
                        {
                            string randomImageName = Path.GetRandomFileName() + ".png";
                            string fileSavePath = Path.Combine(folderPath, randomImageName);
                            using (var stream = new FileStream(fileSavePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            user.ProfileImage = randomImageName;
                            await _unitOfWork.AdminStaffRepository.UpdateProfilePicture(id, randomImageName);
                        }
                        else
                        {
                            System.IO.File.Delete(Path.Combine(folderPath, user.ProfileImage));
                            string randomImageName = Path.GetRandomFileName() + ".png";
                            string fileSavePath = Path.Combine(folderPath, randomImageName);
                            using (var stream = new FileStream(fileSavePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            user.ProfileImage = randomImageName;
                            await _unitOfWork.AdminStaffRepository.UpdateProfilePicture(id, randomImageName);
                        }
                        _unitOfWork.Save();
                        serverRespone.IsSuccess = true;
                        serverRespone.Message = "ImageUploaded";
                        serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: UpdateProfilePicture Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }
        public async Task<ServerRespone> GetProfilePicture(int id, string rootPath)
        {
            ServerRespone serverRespone = new ServerRespone();
            serverRespone.IsSuccess = true;
            serverRespone.Message = "NoImage";
            serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
            serverRespone.Result = new byte[0];
            try
            {
                string folderPath = Path.Combine(rootPath, _imageFolder);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                var user = _unitOfWork.AdminStaffRepository.GetById(id);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(user.ProfileImage))
                    {
                        var imgPath = Path.Combine(folderPath, user.ProfileImage);
                        try
                        {
                            using (FileStream fileStream = new FileStream(imgPath, FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] byteImage = memoryStream.ToArray();
                                    serverRespone.IsSuccess = true;
                                    serverRespone.Message = "GetImageSuccess";
                                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                                    serverRespone.Result = byteImage;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                serverRespone.IsSuccess = false;
                serverRespone.Message = "ServerError: GetImage Staff Failed";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;

        }
    }
}
