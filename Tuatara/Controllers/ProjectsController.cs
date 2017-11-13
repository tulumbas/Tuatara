using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tuatara.Services.Dto;
using Tuatara.Services.BL;
using log4net;

namespace Tuatara.Controllers
{
    public class ProjectsController : ApiController
    {
        static readonly ILog _logger = LogManager.GetLogger(typeof(PlaybookController));
        ProjectClientService _service;        

        public ProjectsController(ProjectClientService service)
        {
            _logger.Debug(nameof(ProjectsController) + " created");
            _service = service;
        }

        // Get api/project
        public IEnumerable<ProjectDtoWithParent> Get()
        {
            _logger.Debug(nameof(ProjectsController) + " get");
            return _service.GetAllProjects();
        }

        // Get api/project/12
        public ProjectDto Get(int id, bool withParent = false)
        {
            return _service.Get(id, withParent);
        }

        // Get api/project/?name=foo
        public IEnumerable<ProjectDto> Get(string name)
        {
            return _service.FindByName(name);
        }

        // POST api/values
        public ProjectDto Post([FromBody] ProjectDto value)
        {            
            if (ModelState.IsValid)
            {
                var result = _service.Create(value);
                return result;
            }
            return null;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] ProjectDto value)
        {
            if(ModelState.IsValid && value.ID == id)
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
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
