using CommonClass.Models;
using CommonClass.Models.Request;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiLab.Services.UnitOfWork.Interface;

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
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<AdminPartService> _logger;
        public AdminPartService(IUnitOfWork unitOfWork, IHttpContextAccessor currentContext, ILogger<AdminPartService> logger)
        {
            this._unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ServerRespone> Create(AdminParts data)
        {
            ServerRespone serverRespone = new ServerRespone();
            try
            {
                if (this._unitOfWork.AdminPartRepository.Add(data))
                {
                    this._unitOfWork.Save();
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
                var part = this._unitOfWork.AdminPartRepository.GetById(key);
                if (part != null)
                {
                    if (this._unitOfWork.AdminPartRepository.Remove(part))
                    {
                        this._unitOfWork.Save();
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
                var value = this._unitOfWork.AdminPartRepository.GetById(key);
                serverRespone.IsSuccess = true;
                serverRespone.Message = "GetSingleSuccess";
                serverRespone.HttpStatusCode = HttpStatusCode.OK;
                serverRespone.Result = value;
            }
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
                var value = this._unitOfWork.AdminPartRepository.GetAll();
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
                if (this._unitOfWork.AdminPartRepository.Update(data))
                {
                    this._unitOfWork.Save();
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
