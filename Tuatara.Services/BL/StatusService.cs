using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Services.BL
{
    public class StatusService : BaseService
    {
        static volatile Dictionary<string, PlaybookStatusEntity> _knownStatuses;
        static readonly object _lockObject = new object();
        static readonly string[] _knownStatusesNames = new[] { "Booked", "Rescheduled", "Cancelled", "Completed", "Confirmed" };

        IRepository<PlaybookStatusEntity> _repository;

        public StatusService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _repository = unitOfWork.Repository<PlaybookStatusEntity>();
            InitializeKnownStatuses();
        }

        public PlaybookStatusEntity Booked { get { return this["Booked"]; } }
        public PlaybookStatusEntity Rescheduled { get { return this["Resceduled"]; } }

        private void InitializeKnownStatuses()
        {
            if (_knownStatuses == null)
            {
                lock (_lockObject)
                {
                    if (_knownStatuses == null)
                    {
                        var data = _repository.Query().ToList();
                        _knownStatuses = _knownStatusesNames
                            .Select(n => new { key = n, value = data.FirstOrDefault(x => x.Name == n) })
                            .ToDictionary(x => x.key, x => x.value);
                    }
                }
            }
        }

        public PlaybookStatusEntity this[string name]
        {
            get
            {
                PlaybookStatusEntity s;
                return _knownStatuses.TryGetValue(name, out s) ? s : null;
            }
        }

        protected override void DisposeDisposables()
        {           
        }
    }
}
