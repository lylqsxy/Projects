using AucklandHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Controllers
{
    public class ClassController : Controller
    {
        // GET: List of Classes
        public ActionResult Index()
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                //Declare list of classes
                List<ClassViewModel> list = new List<ClassViewModel>();

                //Get list of subject names
                var subjectName = (from c in db.Classes
                                   join s in db.Subjects
                                       on c.SubjectId equals s.Id into box
                                   from b in box.DefaultIfEmpty()
                                   select new
                                   {
                                       Id = c.Id,
                                       ClassName = c.Name,
                                       SubjectName = b == null ? "" : b.Name
                                   });

                //Get list of teacher names
                var teacherName = (from c in db.Classes
                                   join t in db.Teachers
                                       on c.TeacherId equals t.Id into box
                                   from b in box.DefaultIfEmpty()
                                   select new
                                   {
                                       Id = c.Id,
                                       TeacherName = (b == null) ? "" : b.FirstName + " " + b.LastName
                                   });

                //Get number of enrolments belong to each class
                var enrollmentCount = (from c in db.Classes
                                       join e in db.Enrollments
                                           on c.Id equals e.ClassId into box
                                       from b in box.DefaultIfEmpty()
                                       select new
                                       {
                                           Id = c.Id,
                                           Enrollment = b
                                       }).GroupBy(x => new { x.Id }).Select(x => new
                                       {
                                           Id = x.Key.Id,
                                           EnrollmentCount = x.Where(g => g.Enrollment != null).Distinct().Count()
                                       });

                //Join three above list to get final list by comparing class ids
                list = (from s in subjectName
                        join e in enrollmentCount on s.Id equals e.Id
                        join t in teacherName on s.Id equals t.Id
                        select new ClassViewModel
                        {
                            Id = s.Id,
                            ClassName = s.ClassName,
                            TeacherName = t.TeacherName,
                            SubjectName = s.SubjectName,
                            EnrolmentCount = e.EnrollmentCount
                        }).ToList();

                //Pass list to front end view
                return View(list);
            }
        }

        // GET: New class page or Edit class page based on given Id
        //      Id = 0: new class
        //      Id != 0: edit existed class...however, id check need to be done first 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare model object and set its id
            ClassEditViewModel model = new ClassEditViewModel();
            model.Id = id;

            //Check for id number to decide whether it will be new or edit existed item
            using (var db = new AucklandHighSchoolEntities())
            {
                if (id != 0)
                {
                    var classes = db.Classes.Find(id);

                    if (classes != null)
                    {
                        model.Id = classes.Id;
                        model.Name = classes.Name;
                        model.SelectedSubject = classes.Subject.Id;
                        model.SelectedTeacher = classes.Teacher.Id;
                    }
                }

                // Create teacher list
                model.TeacherList = db.Teachers.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList();

                // Create subject list
                model.SubjectList = db.Subjects.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
            }
            //Pass model to front end view
            return View(model);
        }

        // Post: Receive model from front end view and add new item or update existed item information to database
        [HttpPost]
        public JsonResult Edit(ClassEditViewModel model)
        {
            string errorMSG = "";

            if(model.Name == null)
            {
                errorMSG = "Class Name is required";
            }
            else
            {
                //  Connect to database and update or add item
                using (var db = new AucklandHighSchoolEntities())
                {
                    // Incase the user does not want to modify class name...it means that the class name still remains the same --> the error check will keep this condition
                    string editedClassName = "";
                    if(model.Id != 0)
                    {
                        editedClassName = db.Classes.Find(model.Id).Name;
                    }

                    // If there is an existed class with the same name 
                    if(db.Classes.Where(x => x.Name.ToLower().Equals(model.Name.ToLower())).Count() > 0 && editedClassName.ToLower() != model.Name.ToLower())
                    {
                        errorMSG = "There are an existed class in our system";
                    }
                    else {
                        // Declare new classes object
                        Class classes = new Class();

                        // Edit existed classes information
                        if (model.Id != 0)
                        {
                            classes = db.Classes.Find(model.Id);
                        }

                        // Update classes information
                        classes.Name = model.Name;
                        classes.SubjectId = model.SelectedSubject;
                        classes.TeacherId = model.SelectedTeacher;

                        // Add new classes if it were new
                        if (model.Id == 0)
                        {
                            db.Classes.Add(classes);
                        }

                        // Save database changes 
                        db.SaveChanges();
                    }
                    
                }
            }
            
            return Json(errorMSG, JsonRequestBehavior.AllowGet);
        }

        // Remove classes
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                // Get class object to remove
                var classes = db.Classes.Find(id);

                if (classes != null)
                {
                    // Get all available enrolments belong to that class
                    List<Enrollment> availableEnrolments = new List<Enrollment>();
                    availableEnrolments = classes.Enrollments.ToList();

                    // Remove all enrolments
                    if (availableEnrolments.Count > 0)
                    {
                        foreach (var item in availableEnrolments)
                        {
                            db.Enrollments.Remove(item);
                        }
                    }

                    // Remove class
                    db.Classes.Remove(classes);

                    // Save database changes
                    db.SaveChanges();
                }
            }
            //Re-direct to front end view
            return RedirectToAction("Index");
        }

        // Get detail of particular class
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Class Classes = db.Classes.Find(id);
                
                if (Classes != null)
                {
                    ClassDetailsViewModel model = new ClassDetailsViewModel();
                    model.Name = Classes.Name;
                    model.TeacherId = Classes.TeacherId;
                    model.TeacherName = Classes.Teacher.FirstName + " " + Classes.Teacher.LastName;
                    model.SubjectId = Classes.SubjectId;
                    model.SubjectName = Classes.Subject.Name;

                    model.StudentList = Classes.Enrollments.Select(x => x.Student).Distinct().ToList();

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
    }
}