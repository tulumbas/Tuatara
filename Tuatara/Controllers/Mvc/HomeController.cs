using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuatara.Data;

namespace Tuatara.Controllers.Mvc
{
    public class HomeController : Controller
    {
        static object _initsetup = new
        {
            priorities = GetEnumValues(typeof(Priorities)),
            statuses = GetEnumValues(typeof(Priorities)),
        };

        public HomeController()
        {
            ViewBag.jsObject = _initsetup;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        //public ViewResult Playbook()
        //{
        //    return Playbook(0);
        //}

        public ViewResult Playbook(int id = 0)
        {
            ViewBag.Title = "Playbook";
            ViewBag.jsObject = _initsetup;
            ViewBag.weekShift = id;

            return View();
        }

        public ViewResult Requests()
        {
            ViewBag.Title = "Requests";
            return View();
        }

        private static IEnumerable<object> GetEnumValues(Type tenum)
        {
            var values = Enum.GetValues(tenum);
            foreach (var item in values)
            {
                yield return new { id = (int)item, value = item.ToString() };
            }
        }
    }
}
