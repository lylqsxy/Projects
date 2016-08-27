using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using AucklandHighSchool.Models;
using System.Data.Entity;

namespace AucklandHighSchool.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult ClassList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Classes.Select(x => new ClassViewModel
                    {
                        Id = x.ClassID,
                        Name = x.Name,
                        Subject = x.Subject.Name,
                        Teacher = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        EnrollmentsCount = x.Enrollments.Select(y => y.EnrollmentID).Distinct().Count()
                    }).ToList();

                return View(list);
            }
        }

        public ActionResult EditClass(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var @class = db.Classes.Find(Id);
                ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
                return View(@class);
            }
        }

        [HttpPost]
        public ActionResult EditClass(Class c)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClassList");
            }
        }

        public ActionResult CreateClass()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                Class @class = new Class();
                ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName}).ToList();
                return View(@class); 
            }
        }

        [HttpPost]
        public ActionResult CreateClass(Class c)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(c).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("ClassList");
            }
        }

        public ActionResult DetailClass(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var @class = db.Classes.Where(x => x.ClassID == Id).FirstOrDefault();
                ClassDetailViewModel cdvm = new ClassDetailViewModel()
                {
                    ClassId = @class.ClassID,
                    Name = @class.Name,
                    SubjectProperty = @class.Subject,
                    TeacherProperty = @class.Teacher,
                    StudentList = @class.Enrollments.Select(x => x.Student).ToList()
                };
                return View(cdvm);
            }
        }

        [HttpPost]
        public ActionResult DeleteClass(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var c = db.Classes.Find(Id);
                db.Entry(c).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("ClassList");
            }
        }
    }
}