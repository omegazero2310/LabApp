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
        public async Task<IEnumerable<AdminStaff>> Get([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            try
            {
                return await this._adminStaffsService.Gets(skip, take);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get data failed");
                throw;
            }
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<AdminStaff?> Get([FromQuery] int id)
        {
            try
            {
                return await this._adminStaffsService.Get(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Get data failed");
                throw;
            }
        }
        [HttpGet]
        [ActionName("GetProfilePicture")]
        public async Task<IActionResult> GetPicture([FromQuery] int id)
        {
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var dataImg = await this._adminStaffsService.GetProfilePicture(id, _webHostEnvironment.WebRootPath);
            return File(dataImg, "image/jpeg");
        }

        [HttpPost]
        [ActionName("UploadProfilePicture")]
        public async Task<IActionResult> UploadPicture([FromQuery] int id, IFormFile file)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var result = await this._adminStaffsService.UpdateProfilePicture(id, _webHostEnvironment.WebRootPath, file);
                if (result == true)
                    return Ok();
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Server error: Failed to upload Profile Picture");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Post([FromBody] AdminStaffRequest value)
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
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to add new AdminStaff");
                return StatusCode(500,"Server Error while create new");
            }

        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> Put([FromBody] AdminStaffRequest value)
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
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to update AdminStaff");
                return StatusCode(500, "Server Error while update");
            }         
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await this._adminStaffsService.Delete(id);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to delete AdminStaff");
                return StatusCode(500, "Server Error while delete");
            }

        }
    }
}
