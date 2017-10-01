using System.Collections.Generic;
using System.Web.Http;
using Tuatara.Services.BL;

namespace Tuatara.Controllers
{
    public class ResourceController : ApiController
    {
        ResourceService _service;

        public ResourceController(ResourceService service)
        {
            _service = service;
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
