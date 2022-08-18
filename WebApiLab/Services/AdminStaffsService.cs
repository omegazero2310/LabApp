using CommonClass.Models;
using WebApiLab.DatabaseContext;
using WebApiLab.Exts;

namespace WebApiLab.Services
{

    /// <summary>CRUD trên bảng Admin.Staffs</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 18/08/2022 created
    /// </Modified>
    public class AdminStaffsService : IBackendService<AdminStaff>
    {
        private LabDbContext _labDbContext;
        private IServiceProvider _serviceProvider;
        public AdminStaffsService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._labDbContext = serviceProvider.GetService<LabDbContext>();
        }
        public Task<bool> Create(AdminStaff data)
        {
            if (this._labDbContext?.AdminStaffs.AddIfNotExists(data, db => db.ID == data.ID) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);

        }

        public Task<bool> Delete(object key)
        {
            if (this._labDbContext?.AdminStaffs.DeleteIfExists(new AdminStaff { ID = Convert.ToInt32(key) }, db => db.ID == Convert.ToInt32(key)) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }

        public Task<AdminStaff?> Get(object key)
        {
            return Task.FromResult<AdminStaff?>(this._labDbContext?.AdminStaffs.Where(user => user.ID == Convert.ToInt32(key)).FirstOrDefault());
        }

        public Task<IEnumerable<AdminStaff>> Gets(int skip, int take)
        {
            if (skip > 0 && take > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Skip(skip).Take(take).ToList() ?? new List<AdminStaff>()
                    );
            else if (take > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Take(take).ToList() ?? new List<AdminStaff>()
                    );
            else if (skip > 0)
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.Skip(skip).ToList() ?? new List<AdminStaff>()
                    );
            else
                return Task.FromResult<IEnumerable<AdminStaff>>(
                    this._labDbContext?.AdminStaffs.ToList() ?? new List<AdminStaff>()
                    );
        }

        public Task<bool> Update(AdminStaff data)
        {
            if (this._labDbContext?.AdminStaffs.UpdateIfExists(data, db => db.ID == data.ID) != null)
            {
                this._labDbContext?.SaveChanges();
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }
    }
}
