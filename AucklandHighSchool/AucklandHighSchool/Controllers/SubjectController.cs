using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using System.Data.Entity;
using AucklandHighSchool.Models;

namespace AucklandHighSchool.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult SubjectList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Subjects.Select(x => new SubjectViewModel
                {
                    Id = x.SubjectID,
                    Name = x.Name,
                    ClassCount = x.Classes.Select(y => y.ClassID).Distinct().Count(),
                    TeacherCount = x.Classes.Select(y => y.TeacherID).Distinct().Count(),
                    EnrollmentsCount = x.Classes.SelectMany(y => y.Enrollments.Select(z => z.EnrollmentID)).Distinct().Count()
                }).ToList();

                return View(list);
            }       
        }

        public ActionResult EditSubject(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var subject = db.Subjects.Find(Id);
                return View(subject);
            }
        }

        [HttpPost]
        public ActionResult EditSubject(Subject s)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SubjectList");
            }
        }

        public ActionResult CreateSubject()
        {
            Subject subject = new Subject();
            return View(subject);
        }

        [HttpPost]
        public ActionResult CreateSubject(Subject s)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(s).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("SubjectList");
            }
        }

        public ActionResult DetailSubject(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var subject = db.Subjects.Where(x => x.SubjectID == Id).FirstOrDefault();
                SubjectDetailViewModel sdvm = new SubjectDetailViewModel()
                {
                    Name = subject.Name,
                    ClassList = subject.Classes.Select(x => x.Name).ToList(),
                    TeacherList = subject.Classes.Select(x => x.Teacher.FirstName + " " + x.Teacher.LastName).Distinct().ToList(),
                    StudentList = subject.Classes.SelectMany(x => x.Enrollments.Select(y => y.Student.FirstName + " " + y.Student.LastName)).ToList()
                };
                return View(sdvm);
            }
        }

        [HttpPost]
        public ActionResult DeleteSubject(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var s = db.Subjects.Find(Id);
                db.Entry(s).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("SubjectList");
            }
        }
    }
}