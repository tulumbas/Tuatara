using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuatara.Data.Services
{
    public abstract class BaseService : IDisposable
    {
        private bool _isDisposed;

        protected abstract void DisposeDisposables();

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    DisposeDisposables();
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