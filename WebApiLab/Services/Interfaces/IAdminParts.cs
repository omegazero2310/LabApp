namespace WebApiLab.Services.Interfaces
{
    public interface IAdminParts<T> where T : class
    {
        Task<bool> AddAsync(T user, string userAdd);
        Task<bool> UpdateAsync(T user, string userUpdate);
        Task<bool> DeleteAsync(object key);
        Task<T?> Get(object key);
        Task<IEnumerable<T>> Gets(int skip = 0, int take = 0);
    }
}
