using CommonClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminPartsController : ControllerBase
    {
        private readonly IServiceProvider _configuration;
        private AdminPartService _adminPartService;
        private readonly ILogger<AdminPartsController> _logger;
        public AdminPartsController(IServiceProvider configuration, ILogger<AdminPartsController> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContext)
        {
            this._configuration = configuration;
            this._logger = logger;
            var claims = httpContext.HttpContext.User.Identity.Name;
            this._adminPartService = new AdminPartService(configuration, claims ?? "Unknow");
        }
        // GET: api/<AdminPartsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await this._adminPartService.Gets(0, 0));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex,null);
                return StatusCode(500);
            }
        }

        // GET api/<AdminPartsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await this._adminPartService.Get(id));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }

        // POST api/<AdminPartsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminParts value)
        {
            try
            {
                return Ok(await this._adminPartService.Create(value));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }

        // PUT api/<AdminPartsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AdminParts value)
        {
            try
            {
                return Ok(await this._adminPartService.Update(value));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }

        // DELETE api/<AdminPartsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await this._adminPartService.Delete(id));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return StatusCode(500);
            }
        }
    }
}
