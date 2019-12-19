using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Approvals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Docs()
        {
            return View();
        }

        public ActionResult New()
        {

            return View();
        }

        public ActionResult Access()
        {
            ViewBag.Message = "Access not granted";

            return View();
        }
    }
}