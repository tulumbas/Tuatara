using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class EFRepositoryBase<TEntity> : IRepository<TEntity> 
        where TEntity : class, IBaseEntity
    {
        public EFRepositoryBase(IDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        protected IDbContext _context;
        protected IDbSet<TEntity> Entities { get; private set; }


        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            //var filter = Expression.Lam
            return Entities.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var list = Entities.ToList();
            return list;
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            var query = GetQuery(predicate, orderBy, skip, take);
            return query.ToList();
        }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            IQueryable<TEntity> query = Entities;
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

        public void Add(TEntity entity)
        {
            _context.SetAsAdded(entity);
        }

        public void Update(TEntity entity)
        {
            _context.SetAsModified(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.SetAsDeleted(entity);
        }
    }
}
