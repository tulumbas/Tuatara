using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Tuatara.Models;
using Tuatara.Models.Services;

namespace Tuatara.Controllers
{
    public class ProjectController : ApiController
    {
        IMapper _mapper;
        ProjectClientService _service;

        public ProjectController(IMapper autoMapper, ProjectClientService service)
        {
            _mapper = autoMapper;
            _service = service;
        }

        public IEnumerable<ProjectDto> Get()
        {
            var data = _service.GetAllProjects();
            var result = data.Select(x => _mapper.Map<ProjectDto>(x)).ToList();
            return result;
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
