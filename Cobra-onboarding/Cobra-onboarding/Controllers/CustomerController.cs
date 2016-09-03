﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cobra_onboarding.Models;
using System.Data.Entity;

namespace Cobra_onboarding.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            using (CobraEntities db = new CobraEntities())
            {
                var customers = db.OrderHeaders.Select(x => x.Person).Distinct()
                    .Select(y => new { CustomerId = y.Id, CustomerName = y.Name, Address1 = y.Address1, Address2 = y.Address2, TownCity = y.Town_City }).ToList();
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
                
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person p)
        {
            if(ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(p).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(p);
            }  
        }

        public ActionResult Edit(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                Person p = db.People.Find(Id);
                return View(p);
            }
                
        }

        [HttpPost]
        public ActionResult Edit(Person p)
        {
            if (ModelState.IsValid)
            {
                using (CobraEntities db = new CobraEntities())
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(p);
            }
        }

        public ActionResult Details(int Id)
        {
            using (CobraEntities db = new CobraEntities())
            {
                Person p = db.People.Include("OrderHeaders").Include("OrderHeaders.OrderDetails").Include("OrderHeaders.OrderDetails.Product").Where(x => x.Id == Id).FirstOrDefault();
                ViewBag.Count = p.OrderHeaders.Count();
                return View(p);
            }
        }
    }
}