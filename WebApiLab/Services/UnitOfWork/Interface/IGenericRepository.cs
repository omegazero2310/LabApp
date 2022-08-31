using System.Linq.Expressions;

namespace WebApiLab.Services.UnitOfWork.Interface
{
    /// <summary>
    /// Interface Repository chung cho các bảng
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <Modified>
    /// Name Date Comments
    /// annv3 31/08/2022 created
    /// </Modified>
    public interface IGenericRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        bool Update(T entity);
    }
}
