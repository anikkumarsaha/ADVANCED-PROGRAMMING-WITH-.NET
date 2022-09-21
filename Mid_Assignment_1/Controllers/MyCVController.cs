using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Assignment_1.Controllers
{
    public class MyCVController : Controller
    {
        // GET: MyCV
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult EducationPage()
        {
            return View();
        }

        public ActionResult ProjectPage()
        {
            return View();
        }

        public ActionResult ReferencePage()
        {
            return View();
        }
    }
}