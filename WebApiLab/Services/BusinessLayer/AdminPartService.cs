using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiLab.Services.DataAccessLayer;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.BusinessLayer
{
    /// <summary>
    /// Lớp nghiệp vụ tương tác với bảng AdminParts
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public class AdminPartService : IAdminPartService
    {
        private IAdminParts<AdminParts> _adminPartDAL;
        private string _userName;
        private ILogger<AdminPartService> _logger;
        public AdminPartService(IAdminParts<AdminParts> adminPartsDAL, IHttpContextAccessor currentContext, ILogger<AdminPartService> logger)
        {
            this._adminPartDAL = adminPartsDAL;
            _userName = currentContext.HttpContext.User.Identity.Name ?? "Unknows";
            _logger = logger;
        }
        public async Task<ServerRespone> Create(AdminParts data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (await _adminPartDAL?.AddAsync(data, _userName))
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Created";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminPartService));
                serverRespone.IsSuccess = false;
                serverRespone.Message = "CreateError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
                
            }
            return serverRespone;
        }

        public async Task<ServerRespone> Delete(object key)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (await _adminPartDAL.DeleteAsync(key))
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Deleted";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminPartService));
                serverRespone.IsSuccess = false;
                serverRespone.Message = "DeleteError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
                
            }
            return serverRespone;
        }

        public async Task<ServerRespone> Get(object key)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var value = await _adminPartDAL.Get(key);
                serverRespone.IsSuccess = true;
                serverRespone.Message = "GetSingleSuccess";
                serverRespone.HttpStatusCode = HttpStatusCode.OK;
                serverRespone.Result = value;            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminPartService));
                serverRespone.IsSuccess = false;
                serverRespone.Message = "GetSingleError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;
        }

        public async Task<ServerRespone> Gets(int skip, int take)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                var value = await _adminPartDAL.Gets(skip, take);
                serverRespone.IsSuccess = true;
                serverRespone.Message = "GetsSuccess";
                serverRespone.HttpStatusCode = HttpStatusCode.OK;
                serverRespone.Result = value;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminPartService));
                serverRespone.IsSuccess = false;
                serverRespone.Message = "GetsError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
                
            }
            return serverRespone;
        }

        public async Task<ServerRespone> Update(AdminParts data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (await _adminPartDAL.UpdateAsync(data, _userName))
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "Updated";
                    serverRespone.HttpStatusCode = HttpStatusCode.OK;
                    
                }
                else
                {
                    serverRespone.IsSuccess = true;
                    serverRespone.Message = "NoChange";
                    serverRespone.HttpStatusCode = HttpStatusCode.NoContent;
                }
                    
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, nameof(AdminPartService));
                serverRespone.IsSuccess = false;
                serverRespone.Message = "UpdateError";
                serverRespone.HttpStatusCode = HttpStatusCode.InternalServerError;
            }
            return serverRespone;
        }
    }
}
