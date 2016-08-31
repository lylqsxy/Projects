using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Model1 db = new Model1())
            {
                db.Students.ToList();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}