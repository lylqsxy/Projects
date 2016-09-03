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
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var orders = db.OrderHeaders.Include("Person").ToList().Select(x => new { OrderId = x.OrderId, OrderDate = x.OrderDate.Value.ToString("yyyy-MM-dd"), PersonName = x.Person.Name, PersonId = x.Person.Id.ToString() });
                return Json(orders, JsonRequestBehavior.AllowGet);
            }
                
        }

        public JsonResult PersonInfo()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var people = db.People.Select(x => new  { Id = x.Id, Name = x.Name }).ToList();
                return Json(people, JsonRequestBehavior.AllowGet);
            }
                
        }

        [HttpPost]
        public bool Create(OrderHeader o)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(o).State = EntityState.Added;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public bool Edit(OrderHeader o)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(o).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public JsonResult Details(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                OrderHeader o = db.OrderHeaders.Include("Person").Include("OrderDetails").Include("OrderDetails.Product").Where(x => x.OrderId == Id).FirstOrDefault();
                var productList = o.OrderDetails.Select(x => new { OrderDetailId = x.Id, ProductId = x.ProductId, ProductName = x.Product.Name }).ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public bool AddOrderDetails(OrderDetail od)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(od).State = EntityState.Added;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public ActionResult EditModal(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult PartialEditModal(int Id)
        {
            return View(Id);
        }

        public ActionResult CreateModal()
        {
            return View();
        }

        public ActionResult DetailModal()
        {
            return View();
        }

        [HttpPost]
        public bool Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    var order = db.OrderHeaders.Find(Id);
                    db.Entry(order).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public ActionResult AddProductModal()
        {
            return View();
        }

        [HttpPost]
        public bool AddProductModal(OrderDetail od)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(od).State = EntityState.Added;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public JsonResult ProductInfo()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var productList = db.Products.Select(x => new { ProductId = x.Id, ProductName = x.Name }).ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public bool DelOrderDetails(int id)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    var od = db.OrderDetails.Find(id);
                    db.Entry(od).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}