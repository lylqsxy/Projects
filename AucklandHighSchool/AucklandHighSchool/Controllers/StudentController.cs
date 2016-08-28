using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using AucklandHighSchool.Models;
using AucklandHighSchool.Infrustracture;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace AucklandHighSchool.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Select(x => new StudentViewModel
                {
                    Id = x.StudentID,
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    EnrollmentCount = x.Enrollments.Select(y => y.EnrollmentID).Distinct().Count()
                }).ToList();

                return View(list);
            }
        }

        public ActionResult EditStudent(int Id, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var student = db.Students.Find(Id);
                ViewBag.GenderList = GenderList.CreateGenderList();
                ViewBag.RedirectUrl = RedirectUrl;
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult EditStudent(Student s, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(s).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.GenderList = GenderList.CreateGenderList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(s);
                }
            }
        }

        public ActionResult CreateStudent(string RedirectUrl)
        {
            Student student = new Student();
            ViewBag.GenderList = GenderList.CreateGenderList();
            ViewBag.RedirectUrl = RedirectUrl;
            return View(student);
        }

        [HttpPost]
        public ActionResult CreateStudent(Student s, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(s).State = EntityState.Added;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.GenderList = GenderList.CreateGenderList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(s);
                }
            }
        }

        public ActionResult DetailStudent(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var student = db.Students.Where(x => x.StudentID== Id).FirstOrDefault();
                StudentDetailViewModel sdvm = new StudentDetailViewModel()
                {
                    StudentId = student.StudentID,
                    Name = student.FirstName + " " + student.LastName,
                    Gender = student.Gender == "F" ? "Female" : "Male",
                    ClassList = student.Enrollments.Select(x => x.Class).ToList()
                };
                return View(sdvm);
            }
        }

        [HttpPost]
        public ActionResult DeleteStudent(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var s = db.Students.Find(Id);
                if (s.Enrollments.Any())
                {
                    return RedirectToAction("DeleteStudentConfirm", new { StudentId = Id });
                }
                else
                {
                    db.Entry(s).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("StudentLIst");
                }
            }
        }

        public ActionResult EnrollmentList(int StudentId, int? selectedClassId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var student = db.Students.Include("Enrollments").Include("Enrollments.Class")
                    .Include("Enrollments.Class.Subject").Include("Enrollments.Class.Teacher")
                    .Where(x => x.StudentID == StudentId).FirstOrDefault();
                ViewBag.ClassList = db.Classes.Select(x => new SelectListItem { Value = x.ClassID.ToString(), Text = x.Name, Selected = x.ClassID == selectedClassId ? true :false }).ToList();
                var list = student.Enrollments.Select(x => new EnrollmentViewModel
                {
                    EnrollmentID = x.EnrollmentID,
                    Class = x.Class,
                    Subject = x.Class.Subject,
                    Teacher = x.Class.Teacher,
                    EnrollmentsCount = x.Class.Enrollments.Select(y => y.EnrollmentID).Distinct().Count()
                }).ToList();
                EnrollmentListViewModel elvm = new EnrollmentListViewModel
                {
                    Student = student,
                    Evms = list
                };
                return View(elvm);
            }
        }

        [HttpPost]
        public ActionResult EnrollmentAdd(Enrollment e)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(e).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("EnrollmentList", new { StudentId = e.StudentID, selectedClassId = e.ClassID});
            }
        }

        [HttpPost]
        public ActionResult EnrollmentRemove(int EnrollmentId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var e = db.Enrollments.Find(EnrollmentId);
                int studentID = (int)e.StudentID;
                db.Entry(e).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("EnrollmentList", new { StudentId = studentID });
            }
        }

        public ActionResult DeleteStudentConfirm(int StudentId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var student = db.Students.Include("Enrollments").Include("Enrollments.Class")
                    .Include("Enrollments.Class.Subject").Include("Enrollments.Class.Teacher")
                    .Where(x => x.StudentID == StudentId).FirstOrDefault();
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult DeleteStudentConfirmEnrollmentRemove(int EnrollmentId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var e = db.Enrollments.Find(EnrollmentId);
                int studentId = (int)e.StudentID;
                db.Entry(e).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("DeleteStudentConfirm", new { StudentId = studentId });
            }
        }
    }
}