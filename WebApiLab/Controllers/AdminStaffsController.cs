using CommonClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApiLab.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminStaffsController : ControllerBase
    {
        private readonly string _imageFolder = "ProfileImgs";
        private readonly IServiceProvider _configuration;
        private IBackendService<AdminStaff> _adminStaffsService;
        private readonly ILogger<AdminUsersController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminStaffsController(IServiceProvider configuration, ILogger<AdminUsersController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._adminStaffsService = new AdminStaffsService(configuration);
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IEnumerable<AdminStaff>> Get([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            return await this._adminStaffsService.Gets(skip, take);
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<AdminStaff?> Get([FromQuery] int id)
        {
            return await this._adminStaffsService.Get(id);
        }
        [HttpGet]
        [ActionName("GetProfilePicture")]
        public async Task<IActionResult> GetPicture([FromQuery] int id)
        {
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, _imageFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var user = await this._adminStaffsService.Get(id);
            if (user != null)
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
                            return File(byteImage, "image/jpeg");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Failed to get Profile Picture");
                    return NotFound();
                }
            }
            else
                return NotFound();
        }

        [HttpPost]
        [ActionName("UploadProfilePicture")]
        public async Task<IActionResult> UploadPicture([FromQuery] int id, IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, _imageFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            if (file.Length > 0)
            {
                var user = await this._adminStaffsService.Get(id);
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
                        await this._adminStaffsService.Update(user);
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
                        await this._adminStaffsService.Update(user);
                    }
                    return Ok();
                }
                else
                    return NotFound();
            }
            else
                return BadRequest();

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Post([FromBody] AdminStaff value)
        {
            var result = await this._adminStaffsService.Create(value);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> Put([FromBody] AdminStaff value)
        {
            var result = await this._adminStaffsService.Update(value);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await this._adminStaffsService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
