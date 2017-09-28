using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Tuatara.Data.Services;
using Tuatara.Data.Dto;

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
        public IEnumerable<ProjectDto> Get(int id = 0)
        {
            var data = _service.GetAllProjects();
            if(id != 0)
            {
                data = data.Where(p => p.ID == id);
            }
            var result = data.ToList();
            return result;
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
