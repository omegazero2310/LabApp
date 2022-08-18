using CommonClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLab.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminStaffsController : ControllerBase
    {
        private readonly IServiceProvider _configuration;
        private IBackendService<AdminStaff> _adminStaffsService;
        private ILogger<AdminUsersController> _logger;
        public AdminStaffsController(IServiceProvider configuration, ILogger<AdminUsersController> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._adminStaffsService = new AdminStaffsService(configuration);
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
