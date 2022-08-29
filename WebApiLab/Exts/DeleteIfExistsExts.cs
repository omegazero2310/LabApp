using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApiLab.Exts
{
    public static class DeleteIfExistsExt
    {
        /// <summary>
        /// Xóa dòng nếu tồn tại
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet">The database set.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public static T? DeleteIfExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return exists ? dbSet.Remove(entity).Entity : null;
        }
    }
}
