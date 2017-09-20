using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Models.Services
{
    public class ProjectClientService : BaseService
    {
        IProjectClientRepository _repository;

        public ProjectClientService(IProjectClientRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Work> GetAllProjects()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Work> FindProjects(string search)
        {
            return _repository.Query(p => p.ProjectName.Contains(search));
        }

        public IEnumerable<Work> GetSubProjects(int parentID)
        {
            return _repository.Query(p => p.ParentID == parentID);
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }

}