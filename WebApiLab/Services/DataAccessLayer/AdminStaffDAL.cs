using CommonClass.Models;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    /// <summary>
    /// Lớp DAL bảng AdminStaff
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="WebApiLab.Services.Interfaces.IAdminStaffs&lt;CommonClass.Models.AdminStaff&gt;" />
    public class AdminStaffDAL : LabDbContext, IAdminStaffs<AdminStaff>
    {
        private ILogger<AdminStaffDAL> _logger;
        public AdminStaffDAL(DbContextOptions dbContextOptions, ILogger<AdminStaffDAL> logger) : base(dbContextOptions)
        {
            this._logger = logger;
        }

        public async Task<bool> AddAsync(AdminStaff user, string userUpdate)
        {
            user.UserCreated = userUpdate;
            user.UserModified = userUpdate;
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            var entity = this.AdminStaffs.AddIfNotExists(user, db => db.ID == user.ID);
            if (entity != null)
            {
                await this.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(object key)
        {
            if (this.AdminStaffs.DeleteIfExists(new AdminStaff { ID = Convert.ToInt32(key) }, db => db.ID == Convert.ToInt32(key)) != null)
            {
                await this.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<AdminStaff?> Get(object key)
        {
            if (int.TryParse(key.ToString(), out int value))
                return this.AdminStaffs.Where(user => user.ID == value).FirstOrDefault();
            else
                return null;
        }

        public async Task<IEnumerable<AdminStaff>> Gets(int skip = 0, int take = 0)
        {
            if (skip > 0 && take > 0)
                return this.AdminStaffs.Skip(skip).Take(take).ToList() ?? new List<AdminStaff>();
            else if (take > 0)
                return this.AdminStaffs.Take(take).ToList() ?? new List<AdminStaff>();
            else if (skip > 0)
                return this.AdminStaffs.Skip(skip).ToList() ?? new List<AdminStaff>();
            else
                return this.AdminStaffs.ToList() ?? new List<AdminStaff>();
        }
        /// <summary>
        /// Kiểm tra trùng địa chỉ email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate email] [the specified email]; otherwise, <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<bool> IsDuplicateEmail(string email)
        {
            return this.AdminStaffs.Any(staff => staff.Email == email);
        }
        /// <summary>
        /// kiểm tra trùng địa chỉ email trừ người được update
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="excludeID">The exclude identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate email] [the specified email]; otherwise, <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<bool> IsDuplicateEmail(string email, int excludeID)
        {
            return this.AdminStaffs.Any(staff => staff.Email == email && staff.ID != excludeID);
        }
        /// <summary>
        /// Kiểm tra trùng số điện thoại
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate phone number] [the specified phone number]; otherwise, <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<bool> IsDuplicatePhoneNumber(string phoneNumber)
        {
            return this.AdminStaffs.Any(staff => staff.PhoneNumber == phoneNumber);
        }
        /// <summary>
        /// Kiểm tra trùng số điện thoại khi update
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="excludeID">The exclude identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate phone number] [the specified phone number]; otherwise, <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<bool> IsDuplicatePhoneNumber(string phoneNumber, int excludeID)
        {
            return this.AdminStaffs.Any(staff => staff.PhoneNumber == phoneNumber && staff.ID != excludeID);
        }

        public async Task<bool> UpdateAsync(AdminStaff user , string userUpdate)
        {
            user.UserModified = userUpdate;
            user.DateModified = DateTime.Now;
            var model = this.AdminStaffs.UpdateIfExists(user, db => db.ID == user.ID) ?? null;
            if (model != null)
            {
                this.Entry<AdminStaff>(model).Property("ProfileImage").IsModified = false;
                this.Entry<AdminStaff>(model).Property("UserCreated").IsModified = false;
                this.Entry<AdminStaff>(model).Property("DateCreated").IsModified = false;
                await this.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> UpdateProfilePicture(object key, string pictureName, string userUpdate)
        {
            var staff = this.AdminStaffs.Where(data => data.ID == (int)key).FirstOrDefault();
            if (staff != null)
            {
                staff.ProfileImage = pictureName;
                this.AdminStaffs.Update(staff);
                await this.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
