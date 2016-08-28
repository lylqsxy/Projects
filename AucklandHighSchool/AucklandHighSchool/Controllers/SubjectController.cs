﻿using System;
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

        public ActionResult EditSubject(int Id, string RedirectUrl)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var subject = db.Subjects.Find(Id);
                ViewBag.RedirectUrl = RedirectUrl;
                return View(subject);
            }
        }

        [HttpPost]
        public ActionResult EditSubject(Subject s, string RedirectUrl)
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
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(s);
                }
            }
        }

        public ActionResult CreateSubject(string RedirectUrl)
        {
            Subject subject = new Subject();
            ViewBag.RedirectUrl = RedirectUrl;
            return View(subject);
        }

        [HttpPost]
        public ActionResult CreateSubject(Subject s, string RedirectUrl)
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
                    ViewBag.RedirectUrl = RedirectUrl;
                    return View(s);
                }
            }
        }

        public ActionResult DetailSubject(int Id)
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var subject = db.Subjects.Where(x => x.SubjectID == Id).FirstOrDefault();
                SubjectDetailViewModel sdvm = new SubjectDetailViewModel()
                {
                    SubjectId = subject.SubjectID,
                    Name = subject.Name,
                    ClassList = subject.Classes.ToList(),
                    TeacherList = subject.Classes.Select(x => x.Teacher).Distinct().ToList(),
                    StudentList = subject.Classes.SelectMany(x => x.Enrollments.Select(y => y.Student)).ToList()
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