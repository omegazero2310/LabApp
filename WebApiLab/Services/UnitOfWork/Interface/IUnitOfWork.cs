namespace WebApiLab.Services.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminPartRepository AdminPartRepository { get; }
        IAdminStaffRepository AdminStaffRepository { get; }
        IAdminUserRepository AdminUserRepository { get; }
        int Save();
    }
}
