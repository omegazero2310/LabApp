using CommonClass.Models;
using System.Net;
using WebApiLab.Services.DataAccessLayer;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.BusinessLayer
{
    public class AdminPartService
    {
        private IAdminParts<AdminParts> _adminPartDAL;
        private string _userName;
        public AdminPartService(IServiceProvider serviceProvider, string userName)
        {
            this._adminPartDAL = new AdminPartDAL(serviceProvider);
            _userName = userName;
        }
        public async Task<HttpResponseMessage> Create(AdminParts data)
        {
            
            if (await _adminPartDAL?.AddAsync(data, _userName))
            {
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);


        }

        public async Task<HttpResponseMessage> Delete(object key)
        {
            if (await _adminPartDAL.DeleteAsync(key))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
        }

        public async Task<AdminParts?> Get(object key)
        {
            return await _adminPartDAL.Get(key);
        }

        public async Task<IEnumerable<AdminParts>> Gets(int skip, int take)
        {
            return await _adminPartDAL.Gets(skip, take);
        }

        public async Task<HttpResponseMessage> Update(AdminParts data)
        {
            if (await _adminPartDAL.UpdateAsync(data, _userName))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
        }
    }
}
