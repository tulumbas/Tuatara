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
    public class ResourceController : ApiController
    {
        ResourceService _service;

        public ResourceController(ResourceService service)
        {
            _service = service;
        }

        public IEnumerable<AssignableResource> Get()
        {
            return _service.GetAllBookableResources();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
