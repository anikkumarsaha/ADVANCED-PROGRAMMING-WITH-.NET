using Mid_Assignment_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Assignment_2.Controllers
{
    public static class Global
    {
        public static string G_ID;
        public static string G_DOB;
    }


    public class RegistrationController : Controller
    {
        // GET: Registration
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel rm)
        {
            Global.G_ID = rm.ID;
            Global.G_DOB = rm.DOB;

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(rm);
        }
    }
}