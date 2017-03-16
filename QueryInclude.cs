using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThenInclude.EF6;
using System.Data.Entity;

namespace System.Data.Entity.Include
{
    public static class QueryInclude
    {
        public static IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> path) where TProperty : class
        {
            var includedSource = QueryableExtensions.Include(source, path);
            var propertyName = GetPropertyName(path);
            var s = new IncludableQueryable<TEntity, TProperty>(source, propertyName);
            return s;
        }

        public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            var propertyNameChildLambda = GetPropertyName(navigationPropertyPath);
            if (source.Provider.GetType().Name == "DbQueryProvider")
            {
                var s = ((IncludableQueryable<TEntity, ICollection<TPreviousProperty>>)source);
                var sourceChild = QueryableExtensions.Include(s.IQueryable, $"{s.PropertyName}.{propertyNameChildLambda}");
                return new IncludableQueryable<TEntity, TProperty>(sourceChild, $"{s.PropertyName}.{propertyNameChildLambda}");
            }
            return new IncludableQueryable<TEntity, TProperty>(source, propertyNameChildLambda);
        }

        /// <summary>
        /// Given an expression, extract the listed property name; similar to reflection but with familiar LINQ+lambdas.  Technique @via http://stackoverflow.com/a/16647343/1037948
        /// </summary>
        /// <remarks>Cheats and uses the tostring output -- Should consult performance differences</remarks>
        /// <typeparam name="TModel">the model type to extract property names</typeparam>
        /// <typeparam name="TValue">the value type of the expected property</typeparam>
        /// <param name="propertySelector">expression that just selects a model property to be turned into a string</param>
        /// <param name="delimiter">Expression toString delimiter to split from lambda param</param>
        /// <param name="endTrim">Sometimes the Expression toString contains a method call, something like "Convert(x)", so we need to strip the closing part from the end</pa ram >
        /// <returns>indicated property name</returns>
        private static string GetPropertyName<TModel, TValue>(this Expression<Func<TModel, TValue>> propertySelector, char delimiter = '.', char endTrim = ')')
        {

            var asString = propertySelector.ToString(); // gives you: "o => o.Whatever"
            var firstDelim = asString.IndexOf(delimiter); // make sure there is a beginning property indicator; the "." in "o.Whatever" -- this may not be necessary?

            return firstDelim < 0
                ? asString
                : asString.Substring(firstDelim + 1).TrimEnd(endTrim);
        }
    }
}
