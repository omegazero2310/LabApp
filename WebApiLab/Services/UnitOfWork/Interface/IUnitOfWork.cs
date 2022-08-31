namespace WebApiLab.Services.UnitOfWork.Interface
{
    /// <summary>
    /// Unit of work cho database
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        IAdminPartRepository AdminPartRepository { get; }
        IAdminStaffRepository AdminStaffRepository { get; }
        IAdminUserRepository AdminUserRepository { get; }
        int Save();
    }
}
