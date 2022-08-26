using CommonClass.Models;
using CommonClass.Models.Request;
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
        public async Task<ServerRespone> Get()
        {
            try
            {
                return await this._adminPartService.Gets(0, 0);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new ServerRespone { IsSuccess = false, Message = "ServerError", Result = ex, HttpStatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }

        // GET api/<AdminPartsController>/5
        [HttpGet("{id}")]
        public async Task<ServerRespone> Get(int id)
        {
            try
            {
                return await this._adminPartService.Get(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new ServerRespone { IsSuccess = false, Message = "ServerError", Result = ex, HttpStatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }

        // POST api/<AdminPartsController>
        [HttpPost]
        public async Task<ServerRespone> Post([FromBody] AdminParts value)
        {
            try
            {
                return await this._adminPartService.Create(value);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new ServerRespone { IsSuccess = false, Message = "ServerError", Result = ex, HttpStatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }

        // PUT api/<AdminPartsController>/5
        [HttpPut]
        public async Task<ServerRespone> Put([FromBody] AdminParts value)
        {
            try
            {
                return await this._adminPartService.Update(value);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new ServerRespone { IsSuccess = false, Message = "ServerError", Result = ex, HttpStatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }

        // DELETE api/<AdminPartsController>/5
        [HttpDelete("{id}")]
        public async Task<ServerRespone> Delete(int id)
        {
            try
            {
                return await this._adminPartService.Delete(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, null);
                return new ServerRespone { IsSuccess = false, Message = "ServerError", Result = ex, HttpStatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
    }
}
