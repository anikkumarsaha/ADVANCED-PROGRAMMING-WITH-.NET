using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;

namespace ZeroHunger.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Account a)
        {
            var db = new ZeroHungerEntities7();
            db.Accounts.Add(new Account()
            {
                Name = a.Name,
                Password = a.Password,
                Role = "Restaurant",
            });
            db.SaveChanges();

            db.Restaurants.Add(new Restaurant()
            {
                Name = a.Name,
                Location = Request["Location"],
            });
            db.SaveChanges();
            return RedirectToAction("Login");
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account a)
        {
            var db = new ZeroHungerEntities7();
            var user = (from u in db.Accounts
                        where u.Name == a.Name && u.Password == a.Password
                        select u).SingleOrDefault();

            if(user.Role == "Restaurant")
            {
                var AllRestaurants = db.Restaurants.ToList();

                foreach (var item in AllRestaurants)
                {
                    if (item.Name == user.Name)
                    {
                        return RedirectToAction("AllCollectRequest", "Restaurant", new { id = item.Id });
                    }
                }
            }
            else if(user.Role == "Employee")
            {
                var AllEmployees = db.Employees.ToList();

                foreach (var item in AllEmployees)
                {
                    if (item.Name == user.Name)
                    {
                        return RedirectToAction("ShowAssignedTasks", "Employee", new { id = item.Id });
                    }
                }
            }
            else if (user.Role == "NGO")
            {
                var AllNGO = db.NGOs.SingleOrDefault();

                if (AllNGO.Name == user.Name)
                {
                    return RedirectToAction("Index", "NGO", new { id = AllNGO.Id });
                }
            }
            return RedirectToAction("Login");
        }



        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}