using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tuatara.Data.Dto;
using Tuatara.Data.Services;

namespace Tuatara.Controllers
{
    /// <summary>
    /// Retrieves the data associated with playbook view
    /// 
    /// </summary>
    public class PlaybookController : ApiController
    {
        AssignmentService _assignementService;

        public PlaybookController(AssignmentService assignementService)
        {
            _assignementService = assignementService;
        }
                 
        public PlaybookDto Get(int weekShift)
        {
            return _assignementService.GetPlaybookForWeekShift(weekShift);
        }
    }
}
