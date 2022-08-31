using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiLab.Services.BusinessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    /// <summary>
    /// API controller tương tác với bảng Admin.Parts ( chức danh của nhân viên)
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AdminPartsController : ControllerBase
    {
        private readonly IServiceProvider _configuration;
        private AdminPartService _adminPartService;
        private readonly ILogger<AdminPartsController> _logger;
        public AdminPartsController(IServiceProvider configuration, ILogger<AdminPartsController> logger, AdminPartService adminPartService)
        {
            this._configuration = configuration;
            this._logger = logger;        
            this._adminPartService = adminPartService;
        }
        /// <summary>
        /// Lấy danh sách chức danh
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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

        /// <summary>
        /// Lấy chức danh theo ID
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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

        /// <summary>
        /// Tạo chức danh mới
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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

        /// <summary>
        /// Cập nhật chức danh
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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

        /// <summary>
        /// Xóa chức danh
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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
