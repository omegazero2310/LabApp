using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MobileAppLab.ApiServices
{
    public interface IService<T> where T : class
    {
        Task<HttpResponseMessage> CreateNew(T entity);
        Task<IEnumerable<T>> GetAll(int skip = 0, int take = 0);
        Task<T> GetByID(object key);
        Task<HttpResponseMessage> Update(T entity);
        Task<HttpResponseMessage> Delete(object key);
    }
}
