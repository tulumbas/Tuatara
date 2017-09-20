using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Data.Repositories;
using Tuatara.Data.Models;

namespace Tuatara.Models.Services
{
    public class UserService : BaseService
    {
        IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TuataraUser> GetAll()
        {
            return _repository.GetAll();
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }
}