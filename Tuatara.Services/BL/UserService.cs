using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Data.Repositories;
using Tuatara.Data.Entities;

namespace Tuatara.Services.BL
{
    public class UserService : BaseService
    {
        IMapper _mapper;
        IRepository<TuataraUserEntity> _repository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _repository = unitOfWork.Repository<TuataraUserEntity>();
            _mapper = mapper;
        }

        public IEnumerable<TuataraUserEntity> GetAll()
        {
            return _repository.GetAll();
        }

        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }
}