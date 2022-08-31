using System.Linq.Expressions;
using WebApiLab.DatabaseContext;

namespace WebApiLab.Services.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LabDbContext Context;
        protected readonly ILogger<GenericRepository<T>> _logger;
        public GenericRepository(LabDbContext context, ILogger<GenericRepository<T>> logger)
        {
            this.Context = context;
            this._logger = logger;
        }
        public virtual bool Add(T entity, string user="")
        {
            try
            {
                Context.Set<T>().Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                return false;
            }
                 
        }
        public virtual bool AddRange(IEnumerable<T> entities, string user = "")
        {
            try
            {
                Context.Set<T>().AddRange(entities);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                return false;
            }
            
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public virtual T GetById(object id)
        {
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
                this._logger.LogError(ex, this.GetType().Name);
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
                this._logger.LogError(ex, this.GetType().Name);
                return false;
            }
            
        }

        public virtual bool Update(T entity, string userUpdate = "")
        { 
            try
            {
                Context.Set<T>().Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, this.GetType().Name);
                return false;
            }
        }
    }
}
