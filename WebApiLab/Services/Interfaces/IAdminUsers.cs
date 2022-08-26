namespace WebApiLab.Services.Interfaces
{
    public interface IAdminUsers<T> where T : class
    {
        Task<bool> AddAsync(T user);
        Task<bool> UpdateAsync(T user, string _userName);
        Task<bool> DeleteAsync(object key);
        Task<T?> Get(object key);
        Task<IEnumerable<T>> Gets(int skip = 0, int take = 0);
    }
}
