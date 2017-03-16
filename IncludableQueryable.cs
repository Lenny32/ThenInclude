using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ThenInclude.EF6
{
    internal class IncludableQueryable<TEntity, TProperty> : IIncludableQueryable<TEntity, TProperty>
    {
        private readonly IQueryable<TEntity> _queryable;
        internal IQueryable<TEntity> IQueryable => _queryable;
        public IncludableQueryable(IQueryable<TEntity> queryable, string propertyName)
        {
            _queryable = queryable;
            PropertyName = propertyName;
        }
        public IncludableQueryable<TEntity, TProperty> Parent { get; set; }
        public string PropertyName { get; set; }
        public Expression Expression => _queryable.Expression;
        public Type ElementType => _queryable.ElementType;
        public IQueryProvider Provider => _queryable.Provider;

        public IEnumerator<TEntity> GetEnumerator() => _queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
