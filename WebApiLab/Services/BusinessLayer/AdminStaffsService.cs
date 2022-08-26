using CommonClass.ErrorCodes;
using CommonClass.Models;
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

    /// <summary>CRUD trên bảng Admin.Staffs</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminStaffsService
    {
        private IAdminStaffs<AdminStaff> _adminStaffsDAL;
        private AdminStaffValidator _validationRules;
        private string _userName;
        private readonly string _imageFolder = "ProfileImgs";
        public AdminStaffsService(IServiceProvider serviceProvider, string userName)
        {
            this._adminStaffsDAL = new AdminStaffDAL(serviceProvider);
            _validationRules = new AdminStaffValidator();
            _userName = userName;
        }
        public async Task<HttpResponseMessage> Create(AdminStaff data)
        {
            var result = _validationRules.Validate(data);
            if (!result.IsValid)
            {
                var validationRespone = new HttpResponseMessage(HttpStatusCode.BadRequest);
                validationRespone.ReasonPhrase = JsonConvert.SerializeObject(result.Errors);
                return validationRespone;
            }
            bool isDuplicateEmail = await _adminStaffsDAL.IsDuplicateEmail(data.Email);
            bool isDuplicatePhoneNumber = await _adminStaffsDAL.IsDuplicatePhoneNumber(data.PhoneNumber);
            if (isDuplicateEmail)
            {
                var emailRespone = new HttpResponseMessage(HttpStatusCode.BadRequest);
                emailRespone.ReasonPhrase = AdminStaffErrorCode.DUPLICATE_EMAIL;
                return emailRespone;
            }
            if (isDuplicatePhoneNumber)
            {
                var phoneNumberRespone = new HttpResponseMessage(HttpStatusCode.BadRequest);
                phoneNumberRespone.ReasonPhrase = AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER;
                return phoneNumberRespone;
            }
            if (await _adminStaffsDAL?.AddAsync(data, _userName))
            {
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);


        }

        public async Task<HttpResponseMessage> Delete(object key)
        {
            if (await _adminStaffsDAL.DeleteAsync(key))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
        }

        public async Task<AdminStaff?> Get(object key)
        {
            return await _adminStaffsDAL.Get(key);
        }

        public async Task<IEnumerable<AdminStaff>> Gets(int skip, int take)
        {
            return await _adminStaffsDAL.Gets(skip, take);
        }

        public async Task<HttpResponseMessage> Update(AdminStaff data)
        {
            if (await _adminStaffsDAL.UpdateAsync(data, _userName))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
        }
        public async Task<bool> UpdateProfilePicture(int id, string rootPath, IFormFile file)
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
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        public async Task<byte[]> GetProfilePicture(int id, string rootPath)
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
                                return byteImage;//File(byteImage, "image/jpeg")
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                    return new byte[0];

            }
            else
                return new byte[0];
        }
    }
}
