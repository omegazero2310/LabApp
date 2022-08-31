using System.Linq.Expressions;

namespace WebApiLab.Services.UnitOfWork
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        bool Add(T entity, string userUpdate ="");
        bool AddRange(IEnumerable<T> entities, string userUpdate = "");
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        bool Update (T entity, string userUpdate = "");
    }
}
