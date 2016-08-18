using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult SubjectList()
        {
            using (AucklandHighSchoolEntities db  = new AucklandHighSchoolEntities())
            {
                return View(db.Subjects.Include("Classes").ToList());
            }
            
        }

        public ActionResult TeacherList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Teachers.Include("Classes").Select(x => new TeacherViewModel { Name = x.FirstName + " " + x.LastName, Gender = x.Gender}).ToList();
                return View(list);
            }
        }

        public ActionResult StudentList()
        {
            using(AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Include("Enrollments").Select( x => new StudentViewModel { Name = x.FirstName + " " + x.LastName, Gender = x.Gender}).ToList();
                return View(list);
            }
        }

        public ActionResult ClassList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                return View(db.Classes.Include("Subject").Include("Teacher").ToList());
            }
        }

        public ActionResult Test()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Select(x => new TestViewModel { Name = x.FirstName + " " + x.LastName, Number = x.Enrollments.Select(y => y.ClassID).Distinct().Count()}).ToList();
                return View(list);
            } 
        }
    }
}