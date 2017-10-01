using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Repositories;

namespace Tuatara.Services.BL
{
    public abstract class BaseService : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private bool _isDisposed;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected abstract void DisposeDisposables();

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    DisposeDisposables();
                    UnitOfWork.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}