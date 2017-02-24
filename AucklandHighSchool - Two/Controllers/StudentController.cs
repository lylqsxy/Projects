using AucklandHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Controllers
{
    public class StudentController : Controller
    {
        // GET: List of Students
        public ActionResult Index()
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                //Declare list of students
                List<StudentViewModel> list = new List<StudentViewModel>();

                //Connect to database and get list
                list = (from s in db.Students
                        join e in db.Enrollments on s.Id equals e.StudentId into box
                        from b in box.DefaultIfEmpty()
                        select new
                        {
                            Student = s,
                            Enrollment = b
                        }).GroupBy(x => new { x.Student }).Select(x => new StudentViewModel
                        {
                            Id = x.Key.Student.Id,
                            Name = x.Key.Student.FirstName + " " + x.Key.Student.LastName,
                            Gender = x.Key.Student.Gender,
                            EnrolmentCount = x.Where(g => g.Enrollment != null).Distinct().Count()
                        }).ToList();

                //Pass list to front end view
                return View(list);
            }
        }

        // GET: New student page or Edit student page based on given Id
        //      Id = 0: new student
        //      Id != 0: edit existed student...however, id check need to be done first 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare model object and set its id
            StudentEditViewModel model = new StudentEditViewModel();
            model.Id = id;

            //Check for id number to decide whether it will be new or edit existed item
            if (id != 0)
            {
                using (var db = new AucklandHighSchoolEntities())
                {
                    var student = db.Students.Find(id);

                    if (student != null)
                    {
                        model.Id = student.Id;
                        model.FirstName = student.FirstName;
                        model.LastName = student.LastName;
                        model.Gender = student.Gender;
                    }
                }
            }

            //Pass model to front end view
            return View(model);
        }

        // Post: Receive model from front end view and add new item or update existed item information to database
        [HttpPost]
        public ActionResult Edit(StudentEditViewModel model)
        {
            //  DO error checking first
            //  If there are any errors (Model State is not valid) return to front end view with error messages
            //  If not connect to database
            if (ModelState.IsValid)
            {
                //  Connect to database and update or add item
                using (var db = new AucklandHighSchoolEntities())
                {
                    // Declare new student object
                    Student student = new Student();

                    // Edit existed student information
                    if (model.Id != 0)
                    {
                        student = db.Students.Find(model.Id);
                    }

                    // Update student information
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.Gender = model.Gender;

                    // Add new student if it were new
                    if (model.Id == 0)
                    {
                        db.Students.Add(student);
                    }

                    // Save database changes 
                    db.SaveChanges();
                }
                //Re-direct to front end view
                return RedirectToAction("Index");
            }
            else
            {
                //Pass model to front end view
                return View(model);
            }
        }

        // Remove student
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                // Get student object to remove
                var student = db.Students.Find(id);

                if (student != null)
                {
                    // Get all available enrolments belong to that student
                    List<Enrollment> availableEnrolments = new List<Enrollment>();
                    availableEnrolments = student.Enrollments.ToList();

                    // Remove all enrolments
                    if (availableEnrolments.Count > 0)
                    {
                        foreach (var item in availableEnrolments)
                        {
                            db.Enrollments.Remove(item);
                        }
                    }

                    // Remove student
                    db.Students.Remove(student);

                    // Save database changes
                    db.SaveChanges();
                }
            }
            //Re-direct to front end view
            return RedirectToAction("Index");
        }

        // Get detail of particular student
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Student student = db.Students.Find(id);

                if (student != null)
                {
                    StudentDetailsViewModel model = new StudentDetailsViewModel();
                    model.Id = student.Id;
                    model.Name = student.FirstName + " " + student.LastName;
                    model.Gender = student.Gender;
                    model.ClassList = student.Enrollments.Select(x => x.Class).Distinct().ToList();

                    //Re-direct to front end view
                    return View(model);
                }
                else
                {
                    //Re-direct to index view
                    return RedirectToAction("Index");
                }
            }

        }

        [HttpGet]
        public ActionResult EnrolmentAdd(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Student student = db.Students.Find(id);

                if (student != null)
                {
                    EnrolmentDetailsViewModel model = new EnrolmentDetailsViewModel();
                    model.Id = id;
                    model.SelectedClasses = student.Enrollments.Select(x => x.Class).Distinct().ToList();
                    model.ClassList = db.Classes.AsEnumerable().Where(x => !model.SelectedClasses.Contains(x)).Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).Distinct().ToList() ;

                    //Re-direct to front end view
                   return View(model);
                }
                else
                {
                    //Re-direct to details view
                    return RedirectToAction("Details", id);
                }
            }
            
        }

        [HttpPost]
        public ActionResult EnrolmentAdd(EnrolmentDetailsViewModel model)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Student student = db.Students.Find(model.Id);

                if (student != null)
                {
                    Enrollment enrolment;
                    enrolment = db.Enrollments.Where(x => x.ClassId == model.SelectedClass && x.StudentId == model.Id).FirstOrDefault(); 
                     
                   if (enrolment == null)
                    {
                        enrolment = new Enrollment();
                        enrolment.StudentId = model.Id;
                        enrolment.ClassId = model.SelectedClass;

                        db.Enrollments.Add(enrolment);
                        db.SaveChanges();
                    }     
                }
            }

            //Re-direct to front end view
            return RedirectToAction("EnrolmentAdd", model.Id);
        }

        [HttpGet]
        public ActionResult DeleteEnrolment(int classId, int studentId)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Enrollment enrolment = db.Enrollments.Where(x => x.ClassId == classId && x.StudentId == studentId).FirstOrDefault();
                if (enrolment != null)
                {
                    db.Enrollments.Remove(enrolment);
                    db.SaveChanges();
                }
                //Re-direct to front end view
                return RedirectToAction("EnrolmentAdd/" + studentId);
            }
        }
    }
}