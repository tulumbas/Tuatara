using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;
using Tuatara.Services.Dto;

namespace Tuatara.Services.BL
{
    public class ProjectClientService : BaseService
    {
        IRepository<WorkEntity> Repository { get; }
        IMapper _mapper;

        public ProjectClientService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork) 
        {
            Repository = UnitOfWork.Repository<WorkEntity>();
            _mapper = mapper;
        }

        public ProjectDto Get(int id, bool withParent)
        {
            if(withParent)
            {
                var data = Repository.Get(id);
                return _mapper.Map<ProjectDto>(data);
            }
            else
            {
                Repository.SetIncludes( new string[] { "Parent" });
                var data = Repository.Get(id);
                Repository.SetIncludes(null);
                return _mapper.Map<ProjectDtoWithParent>(data);
            }
        }

        public IEnumerable<ProjectDtoWithParent> GetAllProjects()
        {
            // I could have created a map, but it's safer than letting 
            // Automapper roam over navigation properties
            var data = Repository.Query().Select(x => new ProjectDtoWithParent
            {
                ID = x.ID,
                Name = x.Name,
                ParentID = x.ParentID,
                ParentName = x.Parent.Name
            });
            return data.ToArray();
        }

        public ProjectDto Create(ProjectDto project)
        {
            var entity = _mapper.Map<WorkEntity>(project);
            UnitOfWork.BeginTransaction();
            Repository.Add(entity);
            UnitOfWork.Commit();
            project.ID = entity.ID;
            return project;
        }

        public ProjectDto Update(ProjectDto project)
        {
            var entity = _mapper.Map<WorkEntity>(project);
            UnitOfWork.BeginTransaction();
            Repository.Update(entity);
            UnitOfWork.Commit();
            return project;
        }

        public void Delete(int id)
        {
            var fake = new WorkEntity { ID = id };
            UnitOfWork.BeginTransaction();
            Repository.Delete(fake);
            UnitOfWork.Commit();
        }

        public IEnumerable<ProjectDto> FindByName(string search)
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

                var data = Repository.Query(predicate);
                var result = data.ToList();
                return _mapper.Map<IEnumerable<ProjectDto>>(result);
            }
            else
            {
                return Enumerable.Empty<ProjectDto>();
            }
        }

        public IEnumerable<ProjectDto> GetSubProjects(int parentID)
        {
            var data = Repository.Query(p => p.ParentID == parentID);
            return _mapper.Map<IEnumerable<ProjectDto>>(data.ToArray());
        }

        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }

}