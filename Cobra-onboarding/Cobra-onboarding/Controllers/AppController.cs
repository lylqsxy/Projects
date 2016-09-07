using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cobra_onboarding.Models;
using System.Data.Entity;

namespace Cobra_onboarding.Controllers
{
    public class AppController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerModalContent()
        {
            return View();
        }

        public ActionResult OrderModalContent()
        {
            return View();
        }
     
        public JsonResult List()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var customers = db.People
                    .Select(y => new { Id = y.Id, Name = y.Name, Address1 = y.Address1, Address2 = y.Address2, Town_City = y.Town_City, OrderCount = y.OrderHeaders.Count }).ToList();
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
                
        }

        [HttpPost]
        public int DataPostCustomer(Person p)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(p).State = p.Id == 0 ? EntityState.Added : EntityState.Modified;
                    db.SaveChanges();
                    return p.Id;
                }
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        public int Delete(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                var customer = db.People.Find(Id);
                if (db.OrderHeaders.Any(x => x.PersonId == customer.Id))
                {
                    var ohs = db.OrderHeaders.Where(y => y.PersonId == customer.Id);
                    foreach(var oh in ohs)
                    {
                        if (db.OrderDetails.Any(z => z.OrderId == oh.OrderId))
                        {
                            db.OrderDetails.Where(z => z.OrderId == oh.OrderId).ToList()
                                .ForEach(zz => db.Entry(zz).State = EntityState.Deleted);
                        }
                        db.Entry(oh).State = EntityState.Deleted;
                    }
                }
                db.Entry(customer).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public bool UserVal(String Name)
        {
            using (CobraEntities db = new CobraEntities())
            {
                if(!db.People.Any(x => x.Name == Name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}