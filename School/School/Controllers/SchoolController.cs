using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            var db = new StudentManagementEntities();
            var list = db.Subjects.ToList();
            return View(list);
        }

        public ActionResult CreateStudent()
        {

            return View();
        }
    }
}