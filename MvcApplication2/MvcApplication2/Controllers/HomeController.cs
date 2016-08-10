using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;
using System.Data;
using System.Data.Entity;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        ProductEntities db = new ProductEntities();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Search()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Search(Product product)
        {
            Product test = db.Products.Find(product.Id);

            return View(test);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("Index");
        }

        public ActionResult Detail(int id)
        {
            Product test = db.Products.Find(id);
            return View(test); 
        }

        public ActionResult Delete(int id)
        {
            Product test = db.Products.Find(id);
            db.Products.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Product test = db.Products.Find(id);
            return View(test);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
