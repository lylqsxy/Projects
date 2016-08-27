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
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult TeacherList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Teachers.Select(x => new TeacherViewModel
                {
                    Id = x.TeacherID,
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    ClassCount = x.Classes.Select(y => y.ClassID).Distinct().Count(),
                    SubjectCount = x.Classes.Select(y => y.SubjectID).Distinct().Count()
                }).ToList();

                return View(list);
            }
        }

        public ActionResult EditTeacher(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var teacher = db.Teachers.Find(Id);
                ViewBag.GenderList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "M",
                        Text = "Male",
                    },

                    new SelectListItem
                    {
                        Value = "F",
                        Text = "Female"
                    }
                };
                return View(teacher);
            }
        }

        [HttpPost]
        public ActionResult EditTeacher(Teacher t)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TeacherList");
            }
        }

        public ActionResult CreateTeacher()
        {
            Teacher teacher = new Teacher();
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "M",
                    Text = "Male",
                },
           
                new SelectListItem
                {
                    Value = "F",
                    Text = "Female"
                }
            };
            return View(teacher);
        }

        [HttpPost]
        public ActionResult CreateTeacher(Teacher t)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                db.Entry(t).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("TeacherList");
            }
        }

        public ActionResult DetailTeacher(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var teacher = db.Teachers.Where(x => x.TeacherID == Id).FirstOrDefault();
                TeacherDetailViewModel tdvm = new TeacherDetailViewModel()
                {
                    TeacherId = teacher.TeacherID,
                    Name = teacher.FirstName + " " + teacher.LastName,
                    Gender = teacher.Gender == "F" ? "Female" : "Male",
                    ClassList = teacher.Classes.ToList(),
                    SubjectList = teacher.Classes.Select(x => x.Subject).Distinct().ToList()
                };
                return View(tdvm);
            }
        }

        [HttpPost]
        public ActionResult DeleteTeacher(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var t = db.Teachers.Find(Id);
                db.Entry(t).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("TeacherList");
            }
        }
    }
}