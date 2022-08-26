using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminStaffsController : ControllerBase
    {

        private readonly IServiceProvider _configuration;
        private AdminStaffsService _adminStaffsService;
        private readonly ILogger<AdminUsersController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminStaffsController(IServiceProvider configuration, ILogger<AdminUsersController> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContext)
        {
            this._configuration = configuration;
            this._logger = logger;
            var claims = httpContext.HttpContext.User.Identity.Name;
            this._adminStaffsService = new AdminStaffsService(configuration, claims ?? "Unknow");
            this._webHostEnvironment = webHostEnvironment;
        }

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

        [HttpPost]
        [ActionName("Create")]
        public async Task<ServerRespone> Post([FromBody] AdminStaffRequest value)
        {
            try
            {
                var staff = new AdminStaff();
                staff.ID = value.ID;
                staff.UserName = value.UserName;
                staff.Gender = value.Gender;
                staff.Email = value.Email;
                staff.PhoneNumber = value.PhoneNumber;
                staff.Address = value.Address;
                staff.DepartmentName = "";
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

        [HttpPut]
        [ActionName("Update")]
        public async Task<ServerRespone> Put([FromBody] AdminStaffRequest value)
        {
            try
            {
                var staff = new AdminStaff();
                staff.ID = value.ID;
                staff.UserName = value.UserName;
                staff.Gender = value.Gender;
                staff.Email = value.Email;
                staff.PhoneNumber = value.PhoneNumber;
                staff.Address = value.Address;
                staff.DepartmentName = "";
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
