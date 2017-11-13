using log4net;
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
        static readonly ILog _logger = LogManager.GetLogger(typeof(EFRepositoryBase<TEntity>));

        protected IDbSet<TEntity> Entities { get; }
        protected IDbContext Context { get; }
        protected string[] Includes { get; set; }

        public EFRepositoryBase(IDbContext context)
        {
            _logger.Debug("Repository created");
            Context = context;
            Entities = Context.Set<TEntity>();
            Includes = new string[] { };
        }

        public TEntity Get(int id)
        {
            return FirstOrDefault(x => x.ID == id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return AddIncludes(Entities).FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            var query = GetQuery(predicate, orderBy, skip, take);
            return query;
        }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?))
        {
            IQueryable<TEntity> query = AddIncludes(Entities);

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

        public void SetIncludes(IEnumerable<string> fields)
        {
            Includes = fields == null ? new string[] { } : fields.ToArray();
        }

        private IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query)
        {
            foreach (var item in Includes)
            {
                query = query.Include(item);
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
                    Context.Dispose();
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
            Context.SetAsAdded(entity);
        }

        public void Update(TEntity entity)
        {
            Context.SetAsModified(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.SetAsDeleted(entity);
        }
    }
}
