using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        public static class RepositoryResolver
        {
            static Dictionary<Type, Type> _customRepos = new Dictionary<Type, Type>
            {
                //{ typeof(CalendarItemEntity), typeof(CalendarItemRepository) },
                //{ typeof(TuataraUserEntity), typeof(UserRepository) }
            };

            public static Type ResolveRepository<TEntity>()
            {
                Type tRepo, tEntity = typeof(TEntity);
                if(_customRepos.TryGetValue(tEntity, out tRepo))
                {
                    return tRepo;
                }

                var repositoryType = typeof(EFRepositoryBase<>);
                return repositoryType.MakeGenericType(tEntity);
            }
        }

        private readonly IDbContext _context;
        private bool _disposed;
        private Hashtable _repositories;
                
        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var tRepo = RepositoryResolver.ResolveRepository<TEntity>();
            _repositories.Add(type, Activator.CreateInstance(tRepo, _context));

            return (IRepository<TEntity>)_repositories[type];
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public int Commit()
        {
            return _context.Commit();
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                foreach (IDisposable repository in _repositories.Values)
                {
                    repository.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
