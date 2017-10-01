using AutoMapper;
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
        int _userID = 1;

        public PlaybookController(AssignmentService assignementService)
        {
            _assignementService = assignementService;
        }
                 
        public Playbook Get(int weekShift)
        {
            var playbook = _assignementService.GetPlaybookForWeekShift(weekShift);
            return playbook;
        }

        [HttpPost]
        public void CreateRow(int weekID, PlaybookRow row)
        {
            _assignementService.CreateAssignment(weekID, row, _userID);
        }
    }
}
