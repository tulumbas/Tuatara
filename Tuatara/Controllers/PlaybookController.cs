using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tuatara.Models;
using Tuatara.Models.Services;

namespace Tuatara.Controllers
{
    /// <summary>
    /// Retrieves the data associated with playbook view
    /// 
    /// </summary>
    public class PlaybookController : ApiController
    {
        PlaybookService _playbookService;

        public PlaybookController(PlaybookService playbookService)
        {
            _playbookService = playbookService;
        }
                 
        public PlaybookDto Get(int weekShift)
        {
            return _playbookService.GetPlaybookForWeekShift(weekShift);
        }
    }
}
