using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Services
{
    public class ResourceService : BaseService
    {
        IResourceRepository _repository;

        public ResourceService(IResourceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AssignableResource> GetAllBookableResources()
        {
            return _repository.Query(r => r.IsBookable);
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }

}