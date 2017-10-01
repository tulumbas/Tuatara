using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Services.BL
{
    public class ResourceService : BaseService
    {
        IMapper _mapper;
        IRepository<AssignableResourceEntity> _repository;

        public ResourceService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.Repository<AssignableResourceEntity>();
        }

        public IEnumerable<AssignableResourceEntity> GetAllBookableResources()
        {
            return _repository.Query(r => r.IsBookable);
        }

        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }

}