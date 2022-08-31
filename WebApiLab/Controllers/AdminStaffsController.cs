using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    /// <summary>
    /// API Controller tương tác với bảng Admin.Staffs (danh sách nhân viên)
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminStaffsController : ControllerBase
    {

        private AdminStaffsService _adminStaffsService;
        private readonly ILogger<AdminUsersController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminStaffsController(AdminStaffsService usersService, ILogger<AdminUsersController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._adminStaffsService = usersService;
            this._webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <param name="skip">số bản ghi bỏ qua</param>
        /// <param name="take">số bản ghi lấy</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        /// <response code="200">trả về danh sahcs</response>
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ServerRespone> Get([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            try
            {
                return await this._adminStaffsService.Gets(skip, take);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get data failed");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một người
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        /// <response code="200">trả về một dòng trong bảng nếu có</response>
        [HttpGet]
        [ActionName("Get")]
        public async Task<ServerRespone> Get([FromQuery] int id)
        {
            try
            {
                return await this._adminStaffsService.Get(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get data failed");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }
        /// <summary>
        /// Lấy ảnh đại diện của user
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        [HttpGet]
        [ActionName("GetProfilePicture")]
        public async Task<ServerRespone> GetPicture([FromQuery] int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var data = await this._adminStaffsService.GetProfilePicture(id, _webHostEnvironment.WebRootPath);
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to get Profile Picture");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }

        }
        /// <summary>
        /// Tải ảnh đại diện lên máy chủ
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        [HttpPost]
        [ActionName("UploadProfilePicture")]
        public async Task<ServerRespone> UploadPicture([FromQuery] int id, IFormFile file)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                return await this._adminStaffsService.UpdateProfilePicture(id, _webHostEnvironment.WebRootPath, file);
                
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to upload Profile Picture");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }

        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        [HttpPost]
        [ActionName("Create")]
        public async Task<ServerRespone> Post([FromBody] AdminStaffRequest value)
        {
            try
            {
                var staff = new AdminStaff();
                staff.StaffID = value.ID;
                staff.StaffName = value.UserName;
                staff.Gender = value.Gender;
                staff.Email = value.Email;
                staff.PhoneNumber = value.PhoneNumber;
                staff.Address = value.Address;
                staff.ProfileImage = value.ProfileImage ?? "";
                staff.PartID = value.PartID;
                var result = await this._adminStaffsService.Create(staff);
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to add new AdminStaff");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }

        }
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        [HttpPut]
        [ActionName("Update")]
        public async Task<ServerRespone> Put([FromBody] AdminStaffRequest value)
        {
            try
            {
                var staff = new AdminStaff();
                staff.StaffID = value.ID;
                staff.StaffName = value.UserName;
                staff.Gender = value.Gender;
                staff.Email = value.Email;
                staff.PhoneNumber = value.PhoneNumber;
                staff.Address = value.Address;
                staff.ProfileImage = value.ProfileImage ?? "";
                staff.PartID = value.PartID;
                var result = await this._adminStaffsService.Update(staff);
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to update AdminStaff");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ServerRespone> Delete([FromQuery] int id)
        {
            try
            {
                var result = await this._adminStaffsService.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to delete AdminStaff");
                return new ServerRespone { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, IsSuccess = false, Result = ex, Message = "ServerError" };
            }

        }
    }
}
