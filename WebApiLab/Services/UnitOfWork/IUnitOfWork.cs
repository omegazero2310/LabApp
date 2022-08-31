namespace WebApiLab.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminPartRepository AdminPartRepository { get; }
        IAdminStaffRepository AdminStaffRepository { get; }
        IAdminUserRepository AdminUserRepository { get; }
        int Save();
    }
}
