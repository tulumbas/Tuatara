﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tuatara.Controllers.Mvc
{
    public class RootController : Controller
    {
        // GET: Root
        public ActionResult Index()
        {
            return View();
        }
    }
}