using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tuatara.Data.Dto;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Services
{
    public class ProjectClientService : BaseService
    {
        IProjectClientRepository _repository;
        IMapper _mapper;

        public ProjectClientService(IProjectClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var data = _repository.GetAll();
            return _mapper.Map<IEnumerable<ProjectDto>>(data.ToArray());
        }

        public IEnumerable<ProjectDto> FindProjects(string search)
        {
            if(!string.IsNullOrEmpty(search))
            {
                Expression<Func<Work, bool>> predicate;
                if(search[0] == '*')
                {
                    predicate = p => p.ProjectName.Contains(search);
                }
                else
                {
                    predicate = p => p.ProjectName.StartsWith(search);
                }

                var data = _repository.Query(predicate);
                var result = data.Select(x => _mapper.Map<ProjectDto>(x)).ToList();
                return _mapper.Map<IEnumerable<ProjectDto>>(data.ToArray());
            }
            else
            {
                return Enumerable.Empty<ProjectDto>();
            }
        }

        public IEnumerable<ProjectDto> GetSubProjects(int parentID)
        {
            var data = _repository.Query(p => p.ParentID == parentID);
            return _mapper.Map<IEnumerable<ProjectDto>>(data.ToArray());
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }

}