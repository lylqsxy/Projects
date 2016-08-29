using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using AucklandHighSchool.Models;
using System.Data.Entity;
using PagedList;

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

        public ActionResult EditClass(int Id, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var @class = db.Classes.Find(Id);
                ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
                ViewBag.RedirectUrl = RedirectUrl;
                return View(@class);
            }
        }

        [HttpPost]
        public ActionResult EditClass(Class c, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                    ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(c);
                }   
                
            }
        }

        public ActionResult CreateClass(string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                Class @class = new Class();
                ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName}).ToList();
                ViewBag.RedirectUrl = RedirectUrl;
                return View(@class); 
            }
        }

        [HttpPost]
        public ActionResult CreateClass(Class c, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if(ModelState.IsValid)
                {
                    db.Entry(c).State = EntityState.Added;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name }).ToList();
                    ViewBag.TeacherList = db.Teachers.Select(x => new SelectListItem { Value = x.TeacherID.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(c);
                }
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
        public ActionResult DeleteClass(int Id, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var c = db.Classes.Find(Id);
                if (c.Enrollments.Any())
                {
                    return RedirectToAction("DeleteClassConfirm", new { ClassId = Id, RedirectUrl = RedirectUrl });
                }
                else
                {
                    db.Entry(c).State = EntityState.Deleted;
                    db.SaveChanges();
                    if (string.IsNullOrWhiteSpace(RedirectUrl))
                    {
                        return RedirectToAction("ClassList");
                    }
                    else
                    {
                        return Redirect(RedirectUrl);
                    }
                }
                
            }
        }

        public ActionResult ClassEnrollmentList(int ClassId, int? selectedStudentId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var @class = db.Classes.Include("Enrollments").Include("Enrollments.Student").Where(x => x.ClassID == ClassId).FirstOrDefault();
                ViewBag.StudentList = db.Students.Select(x => new SelectListItem { Value = x.StudentID.ToString(), Text = x.FirstName + " " + x.LastName, Selected = x.StudentID == selectedStudentId ? true : false }).ToList();
                return View(@class);
            }
        }

        [HttpPost]
        public ActionResult EnrollmentAdd(Enrollment e)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(e).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("ClassEnrollmentList", new { ClassId = e.ClassID, selectedStudentId = e.StudentID });
            }
        }

        [HttpPost]
        public ActionResult EnrollmentRemove(int EnrollmentId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var e = db.Enrollments.Find(EnrollmentId);
                int classId = (int)e.ClassID;
                db.Entry(e).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("ClassEnrollmentList", new { ClassId = classId });
            }
        }

        public ActionResult DeleteClassConfirm(int ClassId, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var @class = db.Classes.Include("Enrollments").Include("Enrollments.Student").Where(x => x.ClassID == ClassId).FirstOrDefault();
                ViewBag.RedirectUrl = RedirectUrl;
                return View(@class);
            }
        }

        [HttpPost]
        public ActionResult DeleteClassConfirmEnrollmentRemove(int EnrollmentId, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var e = db.Enrollments.Find(EnrollmentId);
                int classId = (int)e.ClassID;
                db.Entry(e).State = EntityState.Deleted;
                db.SaveChanges();
                return Redirect(RedirectUrl);
            }
        }
    }
}