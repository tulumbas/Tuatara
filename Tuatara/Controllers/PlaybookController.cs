using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tuatara.Services;
using Tuatara.Services.BL;

namespace Tuatara.Controllers
{
    /// <summary>
    /// Retrieves the data associated with playbook view
    /// 
    /// </summary>
    public class PlaybookController : ApiController
    {
        AssignmentService _assignementService;
        ProjectClientService _projectService;

        int _userID = 1;

        public PlaybookController(AssignmentService assignementService, ProjectClientService projectService)
        {
            _assignementService = assignementService;
            _projectService = projectService;
        }
                 
        public Playbook Get(int weekShift)
        {
            var playbook = _assignementService.GetPlaybookForWeekShift(weekShift);
            return playbook;
        }
    }
}
