using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tuatara.Data.Models;
using Tuatara.Data.Services;

namespace Tuatara.Controllers
{
    public class UserController : ApiController
    {
        UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        public IEnumerable<TuataraUser> GetAll()
        {
            return _service.GetAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
