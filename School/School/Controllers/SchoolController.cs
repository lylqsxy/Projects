﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;
using System.Data.Entity;

namespace School.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            using (var db = new StudentManagementEntities())
            {
                var list = db.Subjects.ToList();
                return View(list);
            }
 
        }

        [HttpGet]
        public ActionResult CreateStudent(int Id)
        {
            using (var db = new StudentManagementEntities())
            {
                var std = new Student();
                std.SubjectId = Id;
                return View(std);
            }
  
        }

        [HttpPost]
        public ActionResult CreateStudent(Student Std)
        {
            using (var db = new StudentManagementEntities())
            {
                db.Students.Add(Std);
                db.SaveChanges();

                return RedirectToAction("DisplayAll", "School");
            }
        }

        public ActionResult DisplayAll()
        {
            using (var db = new StudentManagementEntities())
            {
                var List = db.Students.Include("Subject").ToList();

                return View(List);
            }
        }

        [HttpGet]
        public ActionResult EditStudent(int Id)
        {
            using (var db = new StudentManagementEntities())
            {
                var student = db.Students.Where(x => x.ID == Id).FirstOrDefault();
                ViewBag.SubjectId = new SelectList(db.Subjects.ToList(), "Id", "Name", student.SubjectId);
                return View(student);
            }
        }

        [HttpPost]
        public ActionResult EditStudent(Student std)
        {
            using (var db = new StudentManagementEntities())
            {

                //Method 1
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplayAll", "School");

            }
        }
    }
}