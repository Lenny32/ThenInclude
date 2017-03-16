using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThenInclude.EF6;

namespace System.Data.Entity
{
    public static class HelperQueryInclude
    {
        public static IIncludableQueryable<TEntity, TProperty> Including<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> path) where TProperty : class
        {
            return System.Data.Entity.Include.QueryInclude.Include(source, path);
        }
        public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
           this IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source,
           Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
           where TEntity : class
        {
            return System.Data.Entity.Include.QueryInclude.ThenInclude(source, navigationPropertyPath);
        }
    }
}
