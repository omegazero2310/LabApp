using WebApiLab.DatabaseContext;

namespace WebApiLab.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LabDbContext _context;
        public UnitOfWork(LabDbContext context, ILogger<AdminPartRepository> loggerPart, ILogger<AdminStaffRepository> loggerStaff, ILogger<AdminUserRepository> loggerUser)
        {
            this._context = context;
            AdminPartRepository = new AdminPartRepository(this._context, loggerPart);
            AdminStaffRepository = new AdminStaffRepository(this._context, loggerStaff);
            AdminUserRepository = new AdminUserRepository(this._context, loggerUser);
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
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
