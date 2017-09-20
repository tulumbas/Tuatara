using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tuatara.Data.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?));
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
