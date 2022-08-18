﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApiLab.Exts
{
    public static class DeleteIfExistsExt
    {
        public static T? DeleteIfExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return exists ? dbSet.Remove(entity).Entity : null;
        }
    }
}