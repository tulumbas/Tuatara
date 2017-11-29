using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tuatara.Services.Dto;
using Tuatara.Services.BL;
using log4net;
using System.Net.Http;
using System.Net;
using System;

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

        // Get api/projects/12
        public ProjectDto Get(int id, bool withParent = false)
        {            
            return _service.Get(id, withParent);
        }

        // Get api/projects?name=foo
        public IEnumerable<ProjectDto> Get(string name)
        {
            return _service.FindByName(name);
        }

        // POST api/projects/12
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
        public ProjectDto Put(int id, [FromBody] ProjectDto value)
        {
            // deserialization is ok
            if (ModelState.IsValid)                
            {
                // if value is non empty and not fudged
                if (value != null && value.ID == id)
                {
                    try
                    {
                        var result = _service.Update(value);
                        return result;
                    }
                    catch(Exception ex)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                } 
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect or insufficient parameters"));
                }
            }
            else
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
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
