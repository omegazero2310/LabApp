using CommonClass.Models;

namespace WebApiLab.Services.UnitOfWork
{
    public interface IAdminStaffRepository : IGenericRepository<AdminStaff>
    {
        Task<bool> UpdateProfilePicture(object key, string pictureName);
        Task<bool> IsDuplicateEmail(string email);
        Task<bool> IsDuplicateEmail(string email, int id);
        Task<bool> IsDuplicatePhoneNumber(string phoneNumber);
        Task<bool> IsDuplicatePhoneNumber(string phoneNumber, int id);
    }
}
