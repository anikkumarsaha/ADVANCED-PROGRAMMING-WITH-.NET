using Mid_Exam_Scenario_1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Exam_Scenario_1.Controllers
{
    public class PharmacyController : Controller
    {
        // GET: Pharmacy
        public ActionResult Index()
        {
            var db = new MidExamScenario1Entities1();
            var AllMeds = db.Medicines.ToList();
            return View(AllMeds);
        }



        [HttpGet]
        public ActionResult AddMedicine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMedicine(Medicine cs)
        {
            var db = new MidExamScenario1Entities1();
            db.Medicines.Add(cs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult ShowDetails(int id)
        {
            var db = new MidExamScenario1Entities1();
            var med = (from m in db.Medicines
                      where m.Id == id
                      select m).SingleOrDefault();
            return View(med);
        }



        [HttpGet]
        public ActionResult AddGroup(int id)
        {
            Session["MedId"] = id;

            var db = new MidExamScenario1Entities1();
            var AllGroups = db.Groups;
            return View(AllGroups);
        }
        [HttpPost]
        public ActionResult AddGroup(Group g)
        {
            var db = new MidExamScenario1Entities1();
            var MedId = Int32.Parse(Session["MedId"].ToString());

            var Grp = (from m in db.Groups
                      where m.Name == g.Name
                      select m).SingleOrDefault();

            var Med = (from m in db.Medicines
                      where m.Id == MedId
                      select m).SingleOrDefault();

            Med.GroupId = Grp.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult AddSupplier(int id)
        {
            Session["MedId"] = id;

            var db = new MidExamScenario1Entities1();
            var AllSuppliers = db.Suppliers;
            return View(AllSuppliers);
        }
        [HttpPost]
        public ActionResult AddSupplier(Supplier g)
        {
            var db = new MidExamScenario1Entities1();
            var MedId = Int32.Parse(Session["MedId"].ToString());

            var Sup = (from m in db.Suppliers
                       where m.Name == g.Name
                       select m).SingleOrDefault();

            var Med = (from m in db.Medicines
                      where m.Id == MedId
                      select m).SingleOrDefault();

            db.MedicineSuppliers.Add(new MedicineSupplier()
            {
                SupId = Sup.Id,
                MedId = Med.Id,
            });

            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult AddNewGroup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewGroup(Group cs)
        {
            var db = new MidExamScenario1Entities1();
            db.Groups.Add(cs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult AddNewSupplier()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewSupplier (Supplier cs)
        {
            var db = new MidExamScenario1Entities1();
            db.Suppliers.Add(cs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}