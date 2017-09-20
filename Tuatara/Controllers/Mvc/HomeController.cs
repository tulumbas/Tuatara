using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tuatara.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ViewResult Playbook()
        {
            ViewBag.Title = "Playbook";
            return View();
        }

        public ViewResult Requests()
        {
            ViewBag.Title = "Requests";
            return View();
        }
    }
}
