using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tuatara.Data.Dto;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Services.BL
{
    public class ProjectClientService : BaseService
    {
        IRepository<WorkEntity> _repository;
        IMapper _mapper;

        public ProjectClientService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _repository = unitOfWork.Repository<WorkEntity>();
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
                Expression<Func<WorkEntity, bool>> predicate;
                if(search[0] == '*')
                {
                    predicate = p => p.Name.Contains(search);
                }
                else
                {
                    predicate = p => p.Name.StartsWith(search);
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
            //_repository.Dispose();
        }
    }

}