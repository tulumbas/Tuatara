using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tuatara.Data.Entities;

namespace Tuatara.Data.Repositories
{
    public interface IReadOnlyRepository<T> where T : class, IBaseEntity
    {
        T Get(int id);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?));
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void SetIncludes(IEnumerable<string> fields);
    }
}
