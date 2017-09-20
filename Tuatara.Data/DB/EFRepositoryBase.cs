using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public abstract class EFRepositoryBase<T> : IRepository<T> where T : class, new()
    {
        protected TuataraContext _context;
        protected IDbSet<T> Entities { get; private set; }

        protected EFRepositoryBase()
        {
            _context = new TuataraContext();
            Entities = _context.Set<T>();
        }

        public T Get(int id)
        {
            return Entities.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            //var filter = Expression.Lam
            return Entities.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            var list = Entities.ToList();
            return list;
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            var query = GetQuery(predicate, orderBy, skip, take);
            return query.ToList();
        }

        protected IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            IQueryable<T> query = Entities;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        private bool _isDisposed;
        protected void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    _context.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
