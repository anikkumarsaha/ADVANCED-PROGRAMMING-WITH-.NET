using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;

namespace ZeroHunger.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        [HttpGet]
        public ActionResult AllCollectRequest(int id)
        {
            Session["ResID"] = id;
            var db = new ZeroHungerEntities();
            var Requests = (from r in db.CollectRequests
                            where r.RestaurantId == id
                            select r).ToList();
            return View(Requests);
        }



        [HttpGet]
        public ActionResult PendingCollectRequest()
        {
            var ResId = Int32.Parse(Session["ResId"].ToString());
            var db = new ZeroHungerEntities();
            var Requests = (from r in db.CollectRequests
                            where r.RestaurantId == ResId && r.Status == "Requested"
                            select r).ToList();
            return View(Requests);
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cs in db.FoodDetails
                       where cs.Id == id
                       select cs).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Edit(FoodDetail c)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cs in db.FoodDetails
                       where cs.Id == c.Id
                       select cs).SingleOrDefault();
            ext.Name = c.Name;
            ext.Amount = c.Amount;
            ext.CollectRequestId = ext.CollectRequestId;
            db.SaveChanges();
            return RedirectToAction("PendingCollectRequest");
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cs in db.FoodDetails
                       where cs.Id == id
                       select cs).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Delete(FoodDetail c)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cs in db.FoodDetails
                       where cs.Id == c.Id
                       select cs).SingleOrDefault();
            var CollId = ext.CollectRequestId;
            db.FoodDetails.Remove(ext);
            db.SaveChanges();

            var exxt = (from css in db.CollectRequests
                        where css.Id == CollId
                        select css).SingleOrDefault();
            db.CollectRequests.Remove(exxt);
            db.SaveChanges();
            return RedirectToAction("PendingCollectRequest");
        }



        [HttpGet]
        public ActionResult PlaceNewRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PlaceNewRequest(FoodDetail cs)
        {
            var db = new ZeroHungerEntities();

            var ResId = Int32.Parse(Session["ResId"].ToString());
            db.CollectRequests.Add(new CollectRequest()
            {
                Status = "Requested",
                RestaurantId = ResId,
                EmployeeId = null,
                PlacingDate = DateTime.Now,
            });

            db.SaveChanges();

            var CollectReq = db.CollectRequests.ToList().LastOrDefault();

            db.FoodDetails.Add(new FoodDetail()
            {
                Name = cs.Name,
                Amount = cs.Amount,
                CollectRequestId = CollectReq.Id,
            });
            db.SaveChanges();
            return RedirectToAction("AllCollectRequest", new { id = ResId });
        }
    }
}