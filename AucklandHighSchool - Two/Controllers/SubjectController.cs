using AucklandHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Controllers
{
    public class SubjectController : Controller
    {
        // GET: List of Subjects
        public ActionResult Index()
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                //Declare list of subjects
                // List<SubjectViewModel> list = new List<SubjectViewModel>();

                //Get number of classes belong to each subject
                var classCount = (from s in db.Subjects
                                    join c in db.Classes
                                    on s.Id equals c.SubjectId into box
                                    from b in box.DefaultIfEmpty()
                                    select new
                                    {
                                        Subject = s,
                                        Class = b
                                    }).GroupBy(x => new { x.Subject }).Select(x => new
                                    {
                                        Id = x.Key.Subject.Id,
                                        Name = x.Key.Subject.Name,
                                        ClassCount = x.Where(g => g.Class != null).Distinct().Count()
                                    });

                //Get number of teachers belong each subject
                var staffCount = (from s in db.Subjects
                                    join c in db.Classes
                                    on s.Id equals c.SubjectId into box
                                    from b in box.DefaultIfEmpty()
                                    join t in db.Teachers
                                    on b.TeacherId equals t.Id into otherBox
                                    from o in otherBox.DefaultIfEmpty()
                                    select new
                                    {
                                        Subject = s,
                                        Teacher = o
                                    }).GroupBy(x => new { x.Subject }).Select(x => new
                                    {
                                        Id = x.Key.Subject.Id,
                                        StaffCount = x.Where(g => g.Teacher != null).Distinct().Count()
                                    });

                //Get number of enrolments belong each subject
                var enrollmentCount = (from s in db.Subjects
                                        join c in db.Classes
                                        on s.Id equals c.SubjectId into box
                                        from b in box.DefaultIfEmpty()
                                        join e in db.Enrollments
                                        on b.Id equals e.ClassId into otherBox
                                        from o in otherBox.DefaultIfEmpty()
                                        select new
                                        {
                                            Subject = s,
                                            Enrollment = o
                                        }).GroupBy(x => new { x.Subject }).Select(x => new
                                        {
                                            Id = x.Key.Subject.Id,
                                            EnrollmentCount = x.Where(g => g.Enrollment != null).Distinct().Count()
                                        });

                //Join three above list to get final list by comparing subject ids
                var list = (from c in classCount
                        join s in staffCount on c.Id equals s.Id
                        join e in enrollmentCount on c.Id equals e.Id
                        select new SubjectViewModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            ClassCount = c.ClassCount,
                            StaffCount = s.StaffCount,
                            EnrolmentCount = e.EnrollmentCount
                        }).ToList();

                //Pass list to front end view
                return View(list);
            }
        }

        // GET: New subject page or Edit subject page based on given Id
        //      Id = 0: new subject
        //      Id != 0: edit existed subject...however, id check need to be done first 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare model object and set its id
            SubjectEditViewModel model = new SubjectEditViewModel();
            model.Id = id;

            //Check for id number to decide whether it will be new or edit existed item
            if (id != 0)
            {
                using (var db = new AucklandHighSchoolEntities())
                {
                    var subject = db.Subjects.Find(id);

                    if (subject != null)
                    {
                        model.Id = subject.Id;
                        model.Name = subject.Name;
                    }
                }
            }

            //Pass model to front end view
            return View(model);
        }

        // Post: Receive model from front end view and add new item or update existed item information to database
        [HttpPost]
        public JsonResult Edit(SubjectEditViewModel model)
        {
            //  Connect to database and update or add item
            using (var db = new AucklandHighSchoolEntities())
            {
                // Declare new subject object
                Subject subject = new Subject();

                // Edit existed subject information
                if (model.Id != 0)
                {
                    subject = db.Subjects.Find(model.Id);
                }

                // Update subject information
                subject.Name = model.Name;

                // Add new subject if it were new
                if (model.Id == 0)
                {
                    db.Subjects.Add(subject);
                }

                // Save database changes 
                db.SaveChanges();
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        //Check if the subject is already existed 
        [HttpGet]
        public JsonResult SubjectNameCheck(string Name)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                var subjectCount = db.Subjects.Where(x => x.Name.ToLower().Equals(Name.ToLower())).Count();
                
                if(subjectCount > 0)
                {
                    return Json("There are an existed subject in our system", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                
            }
        }

        // Remove subject
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                // Get subject object to remove
                var subject = db.Subjects.Find(id);

                if (subject != null)
                {
                    // Get all available classes belong to that subject
                    List<Class> availableClasses = new List<Class>();
                    availableClasses = subject.Classes.ToList();

                    // Get all available enrolments belong to classes that subject has
                    List<Enrollment> availableEnrolments = new List<Enrollment>();
                    availableEnrolments = (from t in db.Subjects
                                           join c in db.Classes
                                               on t.Id equals c.SubjectId into box
                                           from b in box.DefaultIfEmpty()
                                           join e in db.Enrollments
                                               on b.Id equals e.ClassId into otherBox
                                           from o in otherBox.DefaultIfEmpty()
                                           where o != null
                                           select o).ToList();


                    // Remove all enrolments
                    if (availableEnrolments.Count > 0)
                    {
                        foreach (var item in availableEnrolments)
                        {
                            db.Enrollments.Remove(item);
                        }
                    }

                    // Remove all classes
                    if (availableClasses.Count > 0)
                    {
                        foreach (var item in availableClasses)
                        {
                            db.Classes.Remove(item);
                        }
                    }
                    // Remove subject
                    db.Subjects.Remove(subject);

                    // Save database changes
                    db.SaveChanges();
                }
            }
            //Re-direct to front end view
            return RedirectToAction("Index");
        }

        // Get detail of particular subject
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Subject subject = db.Subjects.Find(id);

                if (subject != null)
                {
                    SubjectDetailsViewModel model = new SubjectDetailsViewModel();
                    model.Name = subject.Name;
                    model.ClassList = subject.Classes.ToList();
                    model.TeacherList = (from s in db.Subjects
                                      join c in db.Classes
                                       on s.Id equals c.SubjectId  into box where s.Id == id
                                      from b in box.DefaultIfEmpty()
                                      join t in db.Teachers
                                        on b.TeacherId equals t.Id into otherBox
                                      from o in otherBox.DefaultIfEmpty()
                                      where o != null
                                         select o).Distinct().ToList();

                    model.StudentList =  (from s in db.Subjects
                                           join c in db.Classes
                                            on s.Id equals c.SubjectId into box where s.Id == id
                                           from b in box.DefaultIfEmpty()
                                           join e in db.Enrollments
                                           on b.Id equals e.ClassId into otherBox
                                           from o in otherBox.DefaultIfEmpty()
                                           where o != null
                                          select o.Student).Distinct().ToList();

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