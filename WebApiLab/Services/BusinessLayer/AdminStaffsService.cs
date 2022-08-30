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
using WebApiLab.Services.DataAccessLayer;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.BusinessLayer
{

    /// <summary>Lớp nghiệp vụ tương tác với bảng AdminStaff</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminStaffsService : IAdminStaffsService
    {
        private IAdminStaffs<AdminStaff> _adminStaffsDAL;
        private AdminStaffValidator _validationRules;
        private string _userName;
        private readonly string _imageFolder = "ProfileImgs";
        public AdminStaffsService(IAdminStaffs<AdminStaff> adminStaffsDAL, IHttpContextAccessor currentContext)
        {
            this._adminStaffsDAL = adminStaffsDAL;
            _validationRules = new AdminStaffValidator();
            _userName = currentContext.HttpContext.User.Identity.Name ?? "Unknows";
        }
        public async Task<ServerRespone> Create(AdminStaff data)
        {
            var result = _validationRules.Validate(data);
            if (!result.IsValid)
            {
                return new ServerRespone { IsSuccess = false, Message = "DataValidateError", HttpStatusCode = HttpStatusCode.BadRequest, Result = result.Errors };
            }
            bool isDuplicateEmail = await _adminStaffsDAL.IsDuplicateEmail(data.Email);
            bool isDuplicatePhoneNumber = await _adminStaffsDAL.IsDuplicatePhoneNumber(data.PhoneNumber);
            if (isDuplicateEmail)
            {
                return new ServerRespone { IsSuccess = false, Message = AdminStaffErrorCode.DUPLICATE_EMAIL, HttpStatusCode = HttpStatusCode.BadRequest, Result = null };
            }
            if (isDuplicatePhoneNumber)
            {
                return new ServerRespone { IsSuccess = false, Message = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER, HttpStatusCode = HttpStatusCode.BadRequest, Result = null };
            }
            if (await _adminStaffsDAL?.AddAsync(data, _userName))
            {
                return new ServerRespone { IsSuccess = true, Message = "Created", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };


        }

        public async Task<ServerRespone> Delete(object key)
        {
            if (await _adminStaffsDAL.DeleteAsync(key))
            {
                return new ServerRespone { IsSuccess = true, Message = "Deleted", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
        }

        public async Task<ServerRespone> Get(object key)
        {
            var value = await _adminStaffsDAL.Get(key);
            return new ServerRespone { IsSuccess = true, Message = "GetSuccess", HttpStatusCode = HttpStatusCode.OK, Result = value };
        }

        public async Task<ServerRespone> Gets(int skip, int take)
        {
            var value = await _adminStaffsDAL.Gets(skip, take);
            return new ServerRespone { IsSuccess = true, Message = "GetsSuccess", HttpStatusCode = HttpStatusCode.OK, Result = value };
        }

        public async Task<ServerRespone> Update(AdminStaff data)
        {
            if (await _adminStaffsDAL.IsDuplicateEmail(data.Email, data.ID))
                return new ServerRespone { IsSuccess = false, Message = AdminStaffErrorCode.DUPLICATE_EMAIL, HttpStatusCode = HttpStatusCode.BadRequest, Result = null };
            if (await _adminStaffsDAL.IsDuplicatePhoneNumber(data.PhoneNumber, data.ID))
                return new ServerRespone { IsSuccess = false, Message = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER, HttpStatusCode = HttpStatusCode.BadRequest, Result = null };
            if (await _adminStaffsDAL.UpdateAsync(data, _userName))
            {
                return new ServerRespone { IsSuccess = true, Message = "Updated", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
        }
        public async Task<ServerRespone> UpdateProfilePicture(int id, string rootPath, IFormFile file)
        {
            string folderPath = Path.Combine(rootPath, _imageFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            if (file.Length > 0)
            {
                var user = await this._adminStaffsDAL.Get(id);
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
                        await _adminStaffsDAL.UpdateProfilePicture(id, randomImageName, _userName);
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
                        await _adminStaffsDAL.UpdateProfilePicture(id, randomImageName, _userName);
                    }
                    return new ServerRespone { IsSuccess = true, Message = "Updated", HttpStatusCode = HttpStatusCode.OK, Result = null };
                }
                else
                    return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
            }
            return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
        }
        public async Task<ServerRespone> GetProfilePicture(int id, string rootPath)
        {
            string folderPath = Path.Combine(rootPath, _imageFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var user = await this._adminStaffsDAL.Get(id);
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
                                return new ServerRespone { IsSuccess = true, Message = "GetImageSuccess", HttpStatusCode = HttpStatusCode.OK, Result = byteImage };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ServerRespone { IsSuccess = false, Message = "GetImageFailed", HttpStatusCode = HttpStatusCode.InternalServerError, Result = new byte[0] };
                    }
                }
                else
                    return new ServerRespone { IsSuccess = true, Message = "NoImage", HttpStatusCode = HttpStatusCode.OK, Result = new byte[0] };

            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoImage", HttpStatusCode = HttpStatusCode.OK, Result = new byte[0] };
        }
    }
}
