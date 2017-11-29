using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tuatara.Controllers.Mvc
{
    public class HomeController : Controller
    {
        //static object _initsetup = new
        //{
        //    priorities = GetEnumValues(typeof(Priorities)),
        //    statuses = GetEnumValues(typeof(Priorities)),
        //};

        public HomeController()
        {
            ViewBag.jsObject = null; // _initsetup;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ViewResult Playbook(int id = 0)
        {
            ViewBag.Title = "Playbook";
            ViewBag.weekShift = id;

            return View();
        }

        public ViewResult Projects()
        {
            ViewBag.Title = "Projects and Products";
            return View();
        }

        public ViewResult Workforce()
        {
            ViewBag.Title = "Resources";
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
