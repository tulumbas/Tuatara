using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tuatara.Services.BL;

namespace Tuatara.Controllers
{
    public class ProjectController : ApiController
    {
        ProjectClientService _service;

        public ProjectController(ProjectClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<string> FindByName(string name)
        {
            return _service.FindProjects(name).Select(x=>x.ProjectName);
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
