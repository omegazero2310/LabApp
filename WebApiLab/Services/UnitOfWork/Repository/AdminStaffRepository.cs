using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.UnitOfWork.Interface;

namespace WebApiLab.Services.UnitOfWork.Repository
{
    public class AdminStaffRepository : GenericRepository<AdminStaff>, IAdminStaffRepository
    {
        public AdminStaffRepository(LabDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override bool Update(AdminStaff entity)
        {
            if (Context.AdminStaffs.Any(staff => staff.StaffID == entity.StaffID))
                return base.Update(entity);
            else
                return false;
        }
        public Task<bool> IsDuplicateEmail(string email)
        {
            return Task.FromResult(Context.AdminStaffs.Any(staff => staff.Email == email));
        }

        public Task<bool> IsDuplicateEmail(string email, int id)
        {
            return Task.FromResult(Context.AdminStaffs.Any(staff => staff.Email == email && staff.StaffID != id));
        }

        public Task<bool> IsDuplicatePhoneNumber(string phoneNumber)
        {
            return Task.FromResult(Context.AdminStaffs.Any(staff => staff.PhoneNumber == phoneNumber));
        }

        public Task<bool> IsDuplicatePhoneNumber(string phoneNumber, int id)
        {
            return Task.FromResult(Context.AdminStaffs.Any(staff => staff.PhoneNumber == phoneNumber && staff.StaffID != id));
        }

        public Task<bool> UpdateProfilePicture(object key, string pictureName)
        {
            var staff = Context.AdminStaffs.Where(data => data.StaffID == (int)key).FirstOrDefault();
            if (staff != null)
            {
                staff.ProfileImage = pictureName;
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
    }
}
