using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThenInclude.EF6
{
    /// <summary>
    ///     Supports queryable Include/ThenInclude chaining operators.
    /// </summary>
    /// <typeparam name="TEntity"> The entity type. </typeparam>
    /// <typeparam name="TProperty"> The property type. </typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IIncludableQueryable<out TEntity, out TProperty> : IQueryable<TEntity>
    {
    }
}
