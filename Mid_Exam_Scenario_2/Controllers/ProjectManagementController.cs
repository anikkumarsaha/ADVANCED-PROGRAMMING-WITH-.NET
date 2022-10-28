using Mid_Exam_Scenario_2.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Exam_Scenario_2.Controllers
{
    public class ProjectManagementController : Controller
    {
        // GET: ProjectManagement
        public ActionResult Index()
        {
            var db = new MidExamScenario2Entities();
            return View(db.Supervisors.ToList());
        }



        [HttpGet]
        public ActionResult CreateProject(int id)
        {
            Session["SupId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project p)
        {
            var db = new MidExamScenario2Entities();
            var SupId = Int32.Parse(Session["SupId"].ToString());

            db.Projects.Add(new Project()
            {
                Name = p.Name,
                SupervisorId = SupId,
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult ProjectList()
        {
            var db = new MidExamScenario2Entities();
            return View(db.Projects.ToList());
        }



        [HttpGet]
        public ActionResult ShowProjectDetails(int id)
        {
            var db = new MidExamScenario2Entities();
            var pro = (from p in db.Projects
                       where p.Id == id
                       select p).SingleOrDefault();
            return View(pro);
        }



        [HttpGet]
        public ActionResult AddMembers(int id)
        {
            Session["ProId"] = id;
            var db = new MidExamScenario2Entities();
            return View(db.Members.ToList());
        }
        [HttpPost]
        public ActionResult AddMembers(int[] members)
        {
            var db = new MidExamScenario2Entities();
            if(members.Length < 4)
            {
                foreach (var item in members)
                {
                    db.ProjectMembers.Add(new ProjectMember()
                    {
                        MemberId = item,
                        ProjectId = Int32.Parse(Session["ProId"].ToString()),
                    });
                }
                db.SaveChanges();
                return RedirectToAction("ProjectList");
            }
            TempData["msg"] = "Every project must have no more than 3 members";
            return RedirectToAction("AddMembers");
        }
    }
}