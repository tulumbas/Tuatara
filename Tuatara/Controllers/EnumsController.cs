using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tuatara.Data;
using Tuatara.Models;

namespace Tuatara.Controllers
{
    public class EnumsController : ApiController
    {
        private IEnumerable<object> GetValues(Type tenum)
        {
            var values = Enum.GetValues(tenum);
            foreach (var item in values)
            {
                yield return new { id = (int)item, value = item.ToString() };
            }
        }

        public IEnumerable<object> GetPriorities()
        {
            return GetValues(typeof(Priorities));
        }

        public IEnumerable<object> GetStatuses()
        {
            return GetValues(typeof(Statuses));
        }
    }
}
