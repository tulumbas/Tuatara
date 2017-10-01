using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Entities;

namespace Tuatara.Data.Repositories
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity;
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        void SetAsModified<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        void BeginTransaction();
        int Commit();
        void Rollback();
    }
}
