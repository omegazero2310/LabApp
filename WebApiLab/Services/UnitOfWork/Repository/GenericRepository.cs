using CommonClass.Models;
using System.Linq.Expressions;
using WebApiLab.DatabaseContext;
using WebApiLab.Services.UnitOfWork.Interface;

namespace WebApiLab.Services.UnitOfWork.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LabDbContext Context;
        protected readonly ILogger _logger;
        public GenericRepository(LabDbContext context, ILogger logger)
        {
            Context = context;
            _logger = logger;
        }
        public virtual bool Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetType().Name);
                return false;
            }

        }
        public virtual bool AddRange(IEnumerable<T> entities)
        {
            try
            {
                Context.Set<T>().AddRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetType().Name);
                return false;
            }

        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList().Where(row => row is IBaseEntity entity ? entity.IsActive : true);
        }
        public virtual T GetById(object id)
        {
            var value = Context.Set<T>().Find(id);
            if (value is IBaseEntity entity)
                return entity.IsActive ? value : null;
            else
                return Context.Set<T>().Find(id);
        }
        public virtual bool Remove(T entity)
        {
            try
            {
                Context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetType().Name);
                return false;
            }

        }
        public virtual bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                Context.Set<T>().RemoveRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetType().Name);
                return false;
            }

        }

        public virtual bool Update(T entity)
        {
            try
            {
                Context.Set<T>().Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetType().Name);
                return false;
            }
        }
    }
}
