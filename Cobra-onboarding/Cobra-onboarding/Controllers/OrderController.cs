using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cobra_onboarding.Models;
using System.Data.Entity;


namespace Cobra_onboarding.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult List()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var orders = db.OrderHeaders.Include("Person").ToList();
                return View(orders);
            }
                
        }

        public ActionResult Create()
        {
            using (CobraEntities db = new CobraEntities())
            {
                ViewBag.PeopleList = db.People.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                return View();
            }
                
        }

        [HttpPost]
        public ActionResult Create(OrderHeader o)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(o).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(o);
            }
        }

        public ActionResult Edit(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                OrderHeader o = db.OrderHeaders.Find(Id);
                ViewBag.PeopleList = db.People.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                return View(o);
            }
        }

        [HttpPost]
        public ActionResult Edit(OrderHeader o)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(o).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(o);
            }
        }

        public ActionResult Details(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                OrderHeader o = db.OrderHeaders.Include("Person").Include("OrderDetails").Include("OrderDetails.Product").Where(x => x.OrderId == Id).FirstOrDefault();
                ViewBag.ProductList = db.Products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                return View(o);
            }
        }

        [HttpPost]
        public ActionResult AddOrderDetails(OrderDetail od)
        {
            using (CobraEntities db = new CobraEntities())
            {
                db.Entry(od).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Details", new { Id = od.OrderId});
            }
        }
    }
}