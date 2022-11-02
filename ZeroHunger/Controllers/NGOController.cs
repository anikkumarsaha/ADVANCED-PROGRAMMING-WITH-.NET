using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;

namespace ZeroHunger.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index()
        {
            var db = new ZeroHungerEntities1();
            return View(db.NGOs.SingleOrDefault());
        }



        [HttpGet]
        public ActionResult AddNewEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewEmployee(Employee cs)
        {
            var db = new ZeroHungerEntities1();
            db.Employees.Add(new Employee()
            {
                Name = cs.Name,
                Location = cs.Location,
                TotalCompletedRequests = 0,
                NGOId = 1,
            });

            var ngo = db.NGOs.SingleOrDefault();
            ngo.TotalEmployees = ngo.TotalEmployees + 1;

            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult EmployeeList()
        {
            var db = new ZeroHungerEntities1();
            return View(db.Employees.ToList());
        }



        public ActionResult AssignTask()
        {
            var db = new ZeroHungerEntities1();
            var Requests = (from r in db.CollectRequests
                            where r.Status == "Requested"
                            select r).ToList();
            return View(Requests);
        }



        public ActionResult AllTasks()
        {
            var db = new ZeroHungerEntities1();
            var Requests = db.CollectRequests.ToList();
            return View(Requests);
        }



        [HttpGet]
        public ActionResult AssignEmployees(int id)
        {
            var db = new ZeroHungerEntities1();
            Session["CollectReqId"] = id;
            return View(db.Employees.ToList());
        }
        [HttpPost]
        public ActionResult AssignEmployees(int[] Employees)
        {
            var db = new ZeroHungerEntities1();
            var CollectReqId = Int32.Parse(Session["CollectReqId"].ToString());

            foreach (var item in Employees)
            {
                db.EmployeeCollectRequests.Add(new EmployeeCollectRequest()
                {
                    EmployeeId = item,
                    CollectRequestId = CollectReqId,
                });
            }
            db.SaveChanges();

            var ex = (from c in db.CollectRequests
                      where c.Id == CollectReqId
                      select c).SingleOrDefault();
            ex.Status = "Assigned";
            ex.RestaurantId = ex.RestaurantId;
            db.SaveChanges();

            return RedirectToAction("AssignTask");
        }
    }
}