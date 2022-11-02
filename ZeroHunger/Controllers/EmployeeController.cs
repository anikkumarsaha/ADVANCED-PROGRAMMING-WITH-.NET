using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var db = new ZeroHungerEntities1();
            return View(db.Employees.ToList());
        }



        public ActionResult ShowAssignedTasks(int id)
        {
            Session["EmpId"] = id;
            var db = new ZeroHungerEntities1();
            var tasks = (from t in db.EmployeeCollectRequests
                         where t.EmployeeId == id
                         select t).ToList();
            return View(tasks);
        }



        public ActionResult ShowPendingTasks()
        {
            var id = Int32.Parse(Session["EmpId"].ToString());
            var db = new ZeroHungerEntities1();
            var tasks = (from t in db.EmployeeCollectRequests
                         where t.EmployeeId == id
                         select t.CollectRequestId);

            var ReqTasks = db.CollectRequests;

            List<CollectRequest> collectList = new List<CollectRequest>();

            foreach (var item in db.CollectRequests)
            {
                foreach (var i in tasks)
                {
                    if(item.Id == i && item.Status == "Assigned")
                    {
                        collectList.Add(item);
                    }
                }
            }

            return View(collectList);
        }


        [HttpGet]
        public ActionResult Complete(int id)
        {
            var db = new ZeroHungerEntities1();
            var ext = (from cs in db.CollectRequests
                       where cs.Id == id
                       select cs).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Complete(CollectRequest c)
        {
            var db = new ZeroHungerEntities1();
            var ext = (from cs in db.CollectRequests
                       where cs.Id == c.Id
                       select cs).SingleOrDefault();
            ext.Status = "Completed";
            ext.RestaurantId = ext.RestaurantId;
            db.SaveChanges();

            var id = Int32.Parse(Session["EmpId"].ToString());

            var ex = (from cs in db.Employees
                       where cs.Id == id
                       select cs).SingleOrDefault();
            ex.TotalCompletedRequests = ex.TotalCompletedRequests+1;
            db.SaveChanges();

            var ngo = db.NGOs.SingleOrDefault();
            ngo.TotalCompletedRequests = ngo.TotalCompletedRequests + 1;
            db.SaveChanges();

            var Emid = Int32.Parse(Session["EmpId"].ToString());

            return RedirectToAction("ShowPendingTasks");
        }
    }
}