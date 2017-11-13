using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;
using Tuatara.Services.Dto;

namespace Tuatara.Services.BL
{
    public class ResourceService : BaseService
    {
        IMapper _mapper;
        IRepository<AssignableResourceEntity> _repository;
        IUnitOfWork _unitOfWork;

        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = unitOfWork.Repository<AssignableResourceEntity>();
        }

        public IEnumerable<ResourceDto> GetAllBookableResources()
        {
            var data = _repository.Query(r => r.IsBookable).ToArray(); 
            // iteration needed to avoid full table scan by mapper
            return _mapper.Map<IEnumerable<ResourceDto>>(data);
        }

        public ResourceDto Get(int id)
        {
            var data = _repository.Get(id);
            return data == null ? null : _mapper.Map<ResourceDto>(data);
        }

        public IEnumerable<ResourceDto> FindByName(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<AssignableResourceEntity, bool>> predicate;
                if (search[0] == '*')
                {
                    predicate = p => p.Name.Contains(search);
                }
                else
                {
                    predicate = p => p.Name.StartsWith(search);
                }

                var data = _repository.Query(predicate, q => q.OrderBy(z => z.Name));
                var result = data.Select(x => _mapper.Map<ResourceDto>(x)).ToList();
                return result;
            }
            else
            {
                return Enumerable.Empty<ResourceDto>();
            }
        }


        public void Create(ResourceDto resource)
        {            
            var entity = _mapper.Map<AssignableResourceEntity>(resource);
            entity.IsBookable = true;

            _unitOfWork.BeginTransaction();
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(ResourceDto resource)
        {
            var entity = _mapper.Map<AssignableResourceEntity>(resource);
            entity.IsBookable = true;

            _unitOfWork.BeginTransaction();
            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var fake = new AssignableResourceEntity { ID = id };

            _unitOfWork.BeginTransaction();
            _repository.Delete(fake);
            _unitOfWork.Commit();
        }


        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }

}