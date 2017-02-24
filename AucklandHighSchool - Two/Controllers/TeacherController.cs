using AucklandHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Controllers
{
    public class TeacherController : Controller
    {
        // GET: List of Teachers
        public ActionResult Index()
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                //Declare list of teachers
                List<TeacherViewModel> list = new List<TeacherViewModel>();

                //Get number of classes each teacher teaches
                var classCount = (from t in db.Teachers
                                  join c in db.Classes
                                    on t.Id equals c.TeacherId into box
                                  from b in box.DefaultIfEmpty()
                                  select new
                                  {
                                      Teacher = t,
                                      Class = b
                                  }).GroupBy(x => new { x.Teacher }).Select(x => new
                                  {
                                      Id = x.Key.Teacher.Id,
                                      Name = x.Key.Teacher.FirstName + " " + x.Key.Teacher.LastName,
                                      Gender = x.Key.Teacher.Gender,
                                      ClassCount = x.Where(g => g.Class != null).Distinct().Count()
                                  });

                //Get number of subjects each teacher teaches
                var subjectCount = (from t in db.Teachers
                                    join c in db.Classes
                                      on t.Id equals c.TeacherId into box
                                    from b in box.DefaultIfEmpty()
                                    join s in db.Subjects
                                    on b.SubjectId equals s.Id into otherBox
                                    from o in otherBox.DefaultIfEmpty()
                                    select new
                                    {
                                        Teacher = t,
                                        Subject = o
                                    }).GroupBy(x => new { x.Teacher }).Select(x => new
                                    {
                                        Id = x.Key.Teacher.Id,
                                        SubjectCount = x.Where(g => g.Subject != null).Distinct().Count()
                                    });

                //Join two above list to get final list by comparing teacher ids
                list = (from c in classCount
                        join s in subjectCount on c.Id equals s.Id
                        select new TeacherViewModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Gender = c.Gender,
                            SubjectCount = s.SubjectCount,
                            ClassCount = c.ClassCount
                        }).ToList();

                //Pass list to front end view
                return View(list);
            }
        }

        // GET: New teacher page or Edit teacher page based on given Id
        //      Id = 0: new teacher
        //      Id != 0: edit existed teacher...however, id check need to be done first 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare model object and set its id
            TeacherEditViewModel model = new TeacherEditViewModel();
            model.Id = id;

            //Check for id number to decide whether it will be new or edit existed item
            if (id != 0)
            {
                using (var db = new AucklandHighSchoolEntities())
                {
                    var teacher = db.Teachers.Find(id);

                    if (teacher != null)
                    {
                        model.Id = teacher.Id;
                        model.FirstName = teacher.FirstName;
                        model.LastName = teacher.LastName;
                        model.Gender = teacher.Gender;
                    }
                }
            }

            //Pass model to front end view
            return View(model);
        }

        // Post: Receive model from front end view and add new item or update existed item information to database
        [HttpPost]
        public ActionResult Edit(TeacherEditViewModel model)
        {
            //  DO error checking first
            //  If there are any errors (Model State is not valid) return to front end view with error messages
            //  If not connect to database
            if (ModelState.IsValid)
            {
                //  Connect to database and update or add item
                using (var db = new AucklandHighSchoolEntities())
                {
                    // Declare new teacher object
                    Teacher teacher = new Teacher();

                    // Edit existed teacher information
                    if (model.Id != 0)
                    {
                        teacher = db.Teachers.Find(model.Id);
                    }

                    // Update teacher information
                    teacher.FirstName = model.FirstName;
                    teacher.LastName = model.LastName;
                    teacher.Gender = model.Gender;

                    // Add new teacher if it were new
                    if (model.Id == 0)
                    {
                        db.Teachers.Add(teacher);
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

        // Remove teacher
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                // Get teacher object to remove
                var teacher = db.Teachers.Find(id);

                if (teacher != null)
                {
                    // Get all available classes belong to that teacher
                    List<Class> availableClasses = new List<Class>();
                    availableClasses = teacher.Classes.ToList();

                    // Get all available enrolments belong to classes that teacher is currently teaching
                    List<Enrollment> availableEnrolments = new List<Enrollment>();
                    availableEnrolments = (from t in db.Teachers
                                           join c in db.Classes
                                               on t.Id equals c.TeacherId into box
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
                    // Remove teacher
                    db.Teachers.Remove(teacher);

                    // Save database changes
                    db.SaveChanges();
                }
            }
            //Re-direct to front end view
            return RedirectToAction("Index");
        }

        // Get detail of particular teacher
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new AucklandHighSchoolEntities())
            {
                Teacher teacher = db.Teachers.Find(id);

                if (teacher != null)
                {
                    TeacherDetailsViewModel model = new TeacherDetailsViewModel();
                    model.Name = teacher.FirstName + " " + teacher.LastName;
                    model.Gender = teacher.Gender;

                    model.SubjectList = (from t in db.Teachers
                                         join c in db.Classes
                                           on t.Id equals c.TeacherId into box where t.Id == id
                                         from b in box.DefaultIfEmpty()
                                         join s in db.Subjects
                                         on b.SubjectId equals s.Id into otherBox
                                         from o in otherBox.DefaultIfEmpty()
                                         where o != null
                                         select o).Distinct().ToList();

                    model.ClassList = (from t in db.Teachers
                                       join c in db.Classes
                                         on t.Id equals c.TeacherId into box where t.Id == id
                                       from b in box.DefaultIfEmpty()
                                       where b != null
                                       select b).Distinct().ToList();
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