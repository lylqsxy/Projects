using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using AucklandHighSchool.Models;
using AucklandHighSchool.Infrustracture;
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

        public ActionResult EditTeacher(int Id, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var teacher = db.Teachers.Find(Id);
                ViewBag.GenderList = GenderList.CreateGenderList();
                ViewBag.RedirectUrl = RedirectUrl;
                return View(teacher);
            }
        }

        [HttpPost]
        public ActionResult EditTeacher(Teacher t, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.GenderList = GenderList.CreateGenderList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(t);
                }  
            }
        }

        public ActionResult CreateTeacher(string RedirectUrl)
        {
            Teacher teacher = new Teacher();
            ViewBag.GenderList = GenderList.CreateGenderList();
            ViewBag.RedirectUrl = RedirectUrl;
            return View(teacher);
        }

        [HttpPost]
        public ActionResult CreateTeacher(Teacher t, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(t).State = EntityState.Added;
                    db.SaveChanges();
                    return Redirect(RedirectUrl);
                }
                else
                {
                    ViewBag.GenderList = GenderList.CreateGenderList();
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(t);
                }
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
                if(t.Classes.Any())
                {
                    return RedirectToAction("DeleteTeacherConfirm", new { TeacherId = Id });
                }
                else
                {
                    db.Entry(t).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("TeacherList");
                }    
            }
        }

        public ActionResult TeacherClassList(int TeacherId, int? selectedSubjectId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var teacher = db.Teachers.Include("Classes").Include("Classes.Subject").Where(x => x.TeacherID == TeacherId).FirstOrDefault();
                ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name, Selected = x.SubjectID == selectedSubjectId ? true : false }).ToList();
                return View(teacher);
            }
        }

        [HttpPost]
        public ActionResult ClassAdd(Class c)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                if(string.IsNullOrWhiteSpace(c.Name))
                {
                    ModelState.AddModelError("ClassName", "Please enter class name");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(c).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("TeacherClassList", new { TeacherId = c.TeacherID, selectedSubjectId = c.SubjectID});
                }
                else
                {
                    var teacher = db.Teachers.Include("Classes").Include("Classes.Subject").Where(x => x.TeacherID == c.TeacherID).FirstOrDefault();
                    ViewBag.SubjectList = db.Subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.Name, Selected = x.SubjectID == c.SubjectID ? true : false}).ToList();
                    return View("TeacherClassList", teacher);
                }
            }
        }

        [HttpPost]
        public ActionResult ClassRemove(int ClassId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var c = db.Classes.Find(ClassId);
                if (c.Enrollments.Any())
                {
                    return RedirectToAction("DeleteClassConfirm", "Class", new { ClassId = c.ClassID });
                }
                else
                {
                    int teacherId = (int)c.TeacherID;
                    db.Entry(c).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("TeacherClassList", new { TeacherId = teacherId });
                }
            }
        }

        public ActionResult DeleteTeacherConfirm(int TeacherId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var teacher = db.Teachers.Include("Classes").Include("Classes.Subject").Where(x => x.TeacherID == TeacherId).FirstOrDefault();
                return View(teacher);
            }
        }

        [HttpPost]
        public ActionResult DeleteTeacherConfirmClassRemove(int ClassId)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var c = db.Classes.Find(ClassId);
                if (c.Enrollments.Any())
                {
                    return RedirectToAction("DeleteClassConfirm", "Class", new { ClassId = c.ClassID });
                }
                else
                {
                    int teacherId = (int)c.TeacherID;
                    db.Entry(c).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("DeleteTeacherConfirm", new { TeacherId = teacherId });
                }
                
            }
        }
    }
}