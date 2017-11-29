using System.Collections.Generic;
using System.Web.Http;
using Tuatara.Services.BL;
using Tuatara.Services.Dto;

namespace Tuatara.Controllers
{
    public class WorkforceController : ApiController
    {
        ResourceService _service;

        public WorkforceController(ResourceService service)
        {
            _service = service;
        }

        // Get api/resources
        public IEnumerable<ResourceDto> Get()
        {
            return _service.GetAllBookableResources();
        }

        // Get api/project/12
        public ResourceDto Get(int id)
        {
            return _service.Get(id);
        }

        // Get api/project/?name=foo
        public IEnumerable<ResourceDto> Get(string name)
        {
            return _service.FindByName(name);
        }

        // POST api/values
        public void Post([FromBody] ResourceDto value)
        {
            if (ModelState.IsValid)
            {
                _service.Create(value);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] ResourceDto value)
        {
            if (ModelState.IsValid && value.ID == id)
            {
                _service.Update(value);
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _service.Delete(id);
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
