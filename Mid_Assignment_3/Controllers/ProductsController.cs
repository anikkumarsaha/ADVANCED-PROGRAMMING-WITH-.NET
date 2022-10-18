using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mid_Assignment_3.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            var db = new Mid_Assignment_3Entities();
            var AllProducts = db.Products.ToList();
            return View(AllProducts);
        }



        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product pt)
        {
            var db = new Mid_Assignment_3Entities();
            db.Products.Add(pt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == id
                       select pt).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == p.Id
                       select pt).SingleOrDefault();
            ext.Name = p.Name;
            ext.Price = p.Price;
            ext.Quantity = p.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == id
                       select pt).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Delete(Product p)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == p.Id
                       select pt).SingleOrDefault();
            db.Products.Remove(ext);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult AddToCart()
        {
            var db = new Mid_Assignment_3Entities();
            var AllProducts = db.Products.ToList();
            return View(AllProducts);
        }



        [HttpGet]
        public ActionResult Cart(int id)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == id
                       select pt).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Cart(Product p)
        {
            var db = new Mid_Assignment_3Entities();
            var ext = (from pt in db.Products
                       where pt.Id == p.Id
                       select pt).SingleOrDefault();

            if(Session["cart"] == null)
            {
                List<Product> products = new List<Product>();
                products.Add(ext);
                string jsonData = new JavaScriptSerializer().Serialize(products);
                Session["cart"] = jsonData;
                return RedirectToAction("AddToCart");
            }
            else
            {
                var jsonData = Session["cart"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<Product>>(jsonData);
                d.Add(ext);
                jsonData = new JavaScriptSerializer().Serialize(d);
                Session["cart"] = jsonData;
                return RedirectToAction("AddToCart");
            }
        }



        [HttpGet]
        public ActionResult Check()
        {
            var jsonData = Session["cart"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<Product>>(jsonData);
            return View(d);
        }
        [HttpPost]
        public ActionResult Check(int[] Id)
        {
            var db = new Mid_Assignment_3Entities();
            foreach (var id in Id)
            {
                var ext = (from pt in db.Products
                           where pt.Id == id
                           select pt).SingleOrDefault();
                db.ConfirmOrders.Add(new ConfirmOrder()
                {
                    Name = ext.Name,
                    Price = ext.Price,
                    Quantity = ext.Quantity, 
                });
            }
                db.SaveChanges();
            return RedirectToAction("AddToCart");
        }



        public ActionResult CheckConfirmed()
        {
            var db = new Mid_Assignment_3Entities();
            var AllProducts = db.ConfirmOrders.ToList();
            return View(AllProducts);
        }
    }
}