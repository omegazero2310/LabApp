using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiLab.Services
{
    public interface IBackendService<T> where T : class
    {
        Task<bool> Create(T data);
        Task<T?> Get(object key);
        Task<IEnumerable<T>> Gets(int skip = 0, int take = 0);
        Task<bool> Update(T data);
        Task<bool> Delete(object key);

    }
}
