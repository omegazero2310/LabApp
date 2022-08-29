using CommonClass.Models;
using CommonClass.Models.Request;
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
    public class AdminPartService
    {
        private IAdminParts<AdminParts> _adminPartDAL;
        private string _userName;
        public AdminPartService(IServiceProvider serviceProvider, string userName)
        {
            this._adminPartDAL = new AdminPartDAL(serviceProvider);
            _userName = userName;
        }
        public async Task<ServerRespone> Create(AdminParts data)
        {

            if (await _adminPartDAL?.AddAsync(data, _userName))
            {
                return new ServerRespone { IsSuccess = true, Message = "Created", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };


        }

        public async Task<ServerRespone> Delete(object key)
        {
            if (await _adminPartDAL.DeleteAsync(key))
            {
                return new ServerRespone { IsSuccess = true, Message = "Deleted", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
        }

        public async Task<ServerRespone> Get(object key)
        {
            var value = await _adminPartDAL.Get(key);
            return new ServerRespone { IsSuccess = true, Message = "GetSuccess", HttpStatusCode = HttpStatusCode.OK, Result = value };
        }

        public async Task<ServerRespone> Gets(int skip, int take)
        {
            var value = await _adminPartDAL.Gets(skip, take);
            return new ServerRespone { IsSuccess = true, Message = "GetsSuccess", HttpStatusCode = HttpStatusCode.OK, Result = value };
        }

        public async Task<ServerRespone> Update(AdminParts data)
        {
            if (await _adminPartDAL.UpdateAsync(data, _userName))
            {
                return new ServerRespone { IsSuccess = true, Message = "Updated", HttpStatusCode = HttpStatusCode.OK, Result = null };
            }
            else
                return new ServerRespone { IsSuccess = true, Message = "NoChange", HttpStatusCode = HttpStatusCode.NoContent, Result = null };
        }
    }
}
