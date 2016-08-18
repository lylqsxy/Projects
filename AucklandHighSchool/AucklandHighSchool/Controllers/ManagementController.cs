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
                List<TeacherList> tl = new List<TeacherList>();
                db.Teachers.Include("Classes").ToList().ForEach(x => tl.Add( new TeacherList { Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
                return View(tl);
            }
        }

        public ActionResult StudentList()
        {
            using(AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                List<StudentList> sl = new List<StudentList>();
                db.Students.Include("Enrollments").ToList().ForEach(x => sl.Add(new StudentList { Name = x.FirstName + " " + x.LastName, Gender = x.Gender}));
                return View(sl);
            }
        }

        public ActionResult ClassList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                return View(db.Classes.Include("Subject").Include("Teacher").ToList());
            }
        }
    }
}