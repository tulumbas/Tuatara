using log4net;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class TuataraContext : DbContext, IDbContext
    {
        static readonly ILog _logger = LogManager.GetLogger(nameof(TuataraContext));
        static readonly ILog _sqlTracer = LogManager.GetLogger("traceSQL");

        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public IDbSet<CalendarItemEntity> CalendarItems { get; private set; }
        public IDbSet<WorkEntity> Works { get; private set; }
        public IDbSet<AssignableResourceEntity> Resources { get; private set; }
        public IDbSet<TuataraUserEntity> Users { get; private set; }
        public IDbSet<AssignmentEntity> Assignments { get; private set; }
        public IDbSet<PlaybookStatusEntity> PlaybookStatuses { get; private set; }
        public IDbSet<IntraweekEntity> Intraweeks { get; private set; }
        public IDbSet<PriorityEntity> Priorities { get; private set; }

        public TuataraContext()
            : base("TuataraContext")
        {
            _logger.Debug("Context created");

            CalendarItems = Set<CalendarItemEntity>();
            Works = Set<WorkEntity>();
            Resources = Set<AssignableResourceEntity>();
            Users = Set<TuataraUserEntity>();
            PlaybookStatuses = Set<PlaybookStatusEntity>();
            Intraweeks = Set<IntraweekEntity>();
            Priorities = Set<PriorityEntity>();
            Assignments = Set<AssignmentEntity>();

            Database.Log = (msg) => _sqlTracer.Debug(msg);
        }

        #region IDbContext

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction()
        {
            this._objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (_objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }
            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public int Commit()
        {
            try
            {
                BeginTransaction();
                var saveChanges = SaveChanges();
                _transaction.Commit();

                return saveChanges;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                BeginTransaction();
                var saveChangesAsync = await SaveChangesAsync();
                _transaction.Commit();

                return saveChangesAsync;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class, IBaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            var dbEntityEntry = Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        public void Detach<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            Entry<TEntity>(entity).State = EntityState.Detached;
        }

        #endregion
    }
}