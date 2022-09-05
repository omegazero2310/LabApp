using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using WebApiLab.DatabaseContext;
using CommonClass.Models;
using WebApiLab.Services.UnitOfWork.Interface;
using WebApiLab.Services.UnitOfWork.Repository;

namespace WebApiLab.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LabDbContext _context;
        private string _currentUser;
        public UnitOfWork(LabDbContext context, ILoggerFactory loggerFactory, IHttpContextAccessor currentContext)
        {
            this._context = context;
            this._currentUser = currentContext.HttpContext?.User.Identity?.Name ?? "CreateRequestAPI";
            AdminPartRepository = new AdminPartRepository(this._context, loggerFactory.CreateLogger("AdminPartRepository"));
            AdminStaffRepository = new AdminStaffRepository(this._context, loggerFactory.CreateLogger("AdminStaffRepository"));
            AdminUserRepository = new AdminUserRepository(this._context, loggerFactory.CreateLogger("AdminUserRepository"));
        }
        public IAdminPartRepository AdminPartRepository
        {
            get;
            private set;
        }
        public IAdminStaffRepository AdminStaffRepository
        {
            get;
            private set;
        }
        public IAdminUserRepository AdminUserRepository
        {
            get;
            private set;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        /// <summary>
        /// Lưu lại dữ liệu các bảng, có lưu lại thông tin người tạo, người sửa, ngày tạo và ngày sửa
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public int Save()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in _context.ChangeTracker.Entries())
            {
                if (changedEntity.Entity is IBaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.DateCreated = now;
                            entity.DateModified = now;
                            entity.UserModified = this._currentUser;
                            entity.UserCreated = this._currentUser;
                            entity.IsActive = true;
                            break;
                        case EntityState.Modified:
                            _context.Entry(entity).Property(x => x.UserCreated).IsModified = false;
                            _context.Entry(entity).Property(x => x.DateCreated).IsModified = false;
                            _context.Entry(entity).Property(x => x.IsActive).IsModified = false;
                            entity.DateModified = now;
                            entity.UserModified = this._currentUser;
                            break;
                        case EntityState.Deleted:
                            _context.Entry(entity).Property(x => x.UserCreated).IsModified = false;
                            _context.Entry(entity).Property(x => x.DateCreated).IsModified = false;
                            _context.Entry(entity).Property(x => x.IsActive).IsModified = true;
                            changedEntity.State = EntityState.Modified;
                            entity.DateModified = now;
                            entity.UserModified = this._currentUser;
                            entity.IsActive = false;
                            break;
                    }
                }
            }
            return _context.SaveChanges();
        }
    }
}
