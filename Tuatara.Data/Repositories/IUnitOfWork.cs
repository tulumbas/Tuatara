using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Entities;

namespace Tuatara.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity;
        void BeginTransaction();
        int Commit();
        void Rollback();
        void Dispose(bool disposing);
    }
}