using CommonClass.Models;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;
using WebApiLab.Services.Interfaces;

namespace WebApiLab.Services.DataAccessLayer
{
    /// <summary>
    /// lớp DAL bảng AdminPart
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="WebApiLab.Services.Interfaces.IAdminParts&lt;CommonClass.Models.AdminParts&gt;" />
    public class AdminPartDAL : LabDbContext, IAdminParts<AdminParts>
    {
        public AdminPartDAL(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public async Task<bool> AddAsync(AdminParts user, string userAdd)
        {
            user.UserCreated = userAdd;
            user.UserModified = userAdd;
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            var entity = this.AdminParts.AddIfNotExists(user, db => db.PartID == user.PartID);
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
            if (this.AdminParts.DeleteIfExists(new AdminParts { PartID = Convert.ToInt32(key) }, db => db.PartID == Convert.ToInt32(key)) != null)
            {
                await this.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<AdminParts?> Get(object key)
        {
            return this.AdminParts.Where(part => part.PartID == (int)key).FirstOrDefault();
        }

        public async Task<IEnumerable<AdminParts>> Gets(int skip = 0, int take = 0)
        {
            if (skip > 0 && take > 0)
                return this.AdminParts.Skip(skip).Take(take).ToList() ?? new List<AdminParts>();
            else if (take > 0)
                return this.AdminParts.Take(take).ToList() ?? new List<AdminParts>();
            else if (skip > 0)
                return this.AdminParts.Skip(skip).ToList() ?? new List<AdminParts>();
            else
                return this.AdminParts.ToList() ?? new List<AdminParts>();
        }

        public async Task<bool> UpdateAsync(AdminParts user, string userUpdate)
        {
            user.UserModified = userUpdate;
            user.DateModified = DateTime.Now;
            var model = this.AdminParts.UpdateIfExists(user, db => db.PartID == user.PartID) ?? null;
            if (model != null)
            {
                this.Entry<AdminParts>(model).Property("UserCreated").IsModified = false;
                this.Entry<AdminParts>(model).Property("DateCreated").IsModified = false;
                await this.SaveChangesAsync();
                return false;
            }
            else
                return false;
        }
    }
}
