namespace WebApiLab.Services.Interfaces
{
    public interface IAdminStaffs<T> where T : class
    {
        Task<bool> AddAsync(T user, string userAdd);
        Task<bool> UpdateAsync(T user, string userUpdate);
        Task<bool> UpdateProfilePicture(object key, string pictureName, string userUpdate);
        Task<bool> DeleteAsync(object key);
        Task<T?> Get(object key);
        Task<IEnumerable<T>> Gets(int skip = 0, int take = 0);
        Task<bool> IsDuplicateEmail(string email);
        Task<bool> IsDuplicateEmail(string email, int id);
        Task<bool> IsDuplicatePhoneNumber(string phoneNumber);
        Task<bool> IsDuplicatePhoneNumber(string phoneNumber, int id);
    }
}
