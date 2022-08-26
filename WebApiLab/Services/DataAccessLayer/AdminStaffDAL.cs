using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    public class AdminStaffDAL : IAdminStaffs<AdminStaff>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        public AdminStaffDAL(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
        }
        public async Task<bool> AddAsync(AdminStaff user, string userUpdate)
        {
            user.UserCreated = userUpdate;
            user.UserModified = userUpdate;
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            var entity = this._labDbContext?.AdminStaffs.AddIfNotExists(user, db => db.ID == user.ID);
            if (entity != null)
            {
                await this._labDbContext?.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(object key)
        {
            if (this._labDbContext?.AdminStaffs.DeleteIfExists(new AdminStaff { ID = Convert.ToInt32(key) }, db => db.ID == Convert.ToInt32(key)) != null)
            {
                await this._labDbContext?.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<AdminStaff?> Get(object key)
        {
            if (int.TryParse(key.ToString(), out int value))
                return this._labDbContext?.AdminStaffs.Where(user => user.ID == value).FirstOrDefault();
            else
                return null;
        }

        public async Task<IEnumerable<AdminStaff>> Gets(int skip = 0, int take = 0)
        {
            if (skip > 0 && take > 0)
                return this._labDbContext?.AdminStaffs.Skip(skip).Take(take).ToList() ?? new List<AdminStaff>();
            else if (take > 0)
                return this._labDbContext?.AdminStaffs.Take(take).ToList() ?? new List<AdminStaff>();
            else if (skip > 0)
                return this._labDbContext?.AdminStaffs.Skip(skip).ToList() ?? new List<AdminStaff>();
            else
                return this._labDbContext?.AdminStaffs.ToList() ?? new List<AdminStaff>();
        }

        public async Task<bool> IsDuplicateEmail(string email)
        {
            return this._labDbContext.AdminStaffs.Any(staff => staff.Email == email);
        }

        public async Task<bool> IsDuplicatePhoneNumber(string phoneNumber)
        {
            return this._labDbContext.AdminStaffs.Any(staff => staff.PhoneNumber == phoneNumber);
        }

        public async Task<bool> UpdateAsync(AdminStaff user , string userUpdate)
        {
            user.UserModified = userUpdate;
            user.DateModified = DateTime.Now;
            var model = this._labDbContext?.AdminStaffs.UpdateIfExists(user, db => db.ID == user.ID) ?? null;
            if (model != null)
            {
                this._labDbContext.Entry<AdminStaff>(model).Property("ProfileImage").IsModified = false;
                this._labDbContext.Entry<AdminStaff>(model).Property("UserCreated").IsModified = false;
                this._labDbContext.Entry<AdminStaff>(model).Property("DateCreated").IsModified = false;
                await this._labDbContext?.SaveChangesAsync();
                return false;
            }
            else
                return false;
        }

        public async Task<bool> UpdateProfilePicture(object key, string pictureName, string userUpdate)
        {
            var staff = this._labDbContext?.AdminStaffs.Where(data => data.ID == (int)key).FirstOrDefault();
            if (staff != null)
            {
                staff.ProfileImage = pictureName;
                this._labDbContext?.AdminStaffs.Update(staff);
                await this._labDbContext?.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
