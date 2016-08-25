using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult SubjectList()
        {
            using (AucklandHighSchoolEntities db  = new AucklandHighSchoolEntities())
            {
                var list = db.Subjects.Include("Classes").Include("Enrollments").Select(x => new SubjectViewModel
                {
                    Name = x.Name,
                    ClassCount = x.Classes.Select(y => y.ClassID).Distinct().Count(),
                    TeacherCount = x.Classes.Select(y => y.TeacherID).Distinct().Count(),
                    EnrollmentsCount = x.Classes.SelectMany(y => y.Enrollments.Select(z => z.EnrollmentID)).Distinct().Count()
                }).ToList();

                var list2 = (from s in db.Subjects
                             join c in db.Classes on s.SubjectID equals c.SubjectID into cbox
                             from cb in cbox.DefaultIfEmpty()
                             join e in db.Enrollments on cb.ClassID equals e.ClassID into ebox
                             from cbb in cbox.DefaultIfEmpty()
                             join t in db.Teachers on cbb.TeacherID equals t.TeacherID into tbox
                             from tb in tbox.DefaultIfEmpty()
                             select new { s, cbox, ebox, tbox })
                            .GroupBy(x => x.s).Select(y => new SubjectViewModel
                            {
                                Name = y.Key.Name,
                                ClassCount = y.SelectMany(z => z.cbox).Select(a => a.ClassID).Distinct().Count(),
                                TeacherCount = y.SelectMany(z => z.tbox).Select(a => a.TeacherID).Distinct().Count(),
                                EnrollmentsCount = y.SelectMany(z => z.ebox).Select(a => a.EnrollmentID).Distinct().Count()
                            }).ToList();

                return View(list);
            }
            
        }

        public ActionResult TeacherList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Teachers.Include("Classes").Select(x => new TeacherViewModel
                {
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    ClassCount = x.Classes.Select(y => y.ClassID).Distinct().Count(),
                    SubjectCount = x.Classes.Select(y => y.SubjectID).Distinct().Count()
                }).ToList();

                var list2 = (from t in db.Teachers
                             join c in db.Classes on t.TeacherID equals c.TeacherID into cbox
                             from cb in cbox.DefaultIfEmpty()
                             join s in db.Subjects on cb.SubjectID equals s.SubjectID into sbox
                             from sb in sbox.DefaultIfEmpty()
                             select new { t, cbox, sbox })
                            .GroupBy(x => x.t).Select(y => new TeacherViewModel
                            {
                                Name = y.Key.FirstName + " " + y.Key.LastName,
                                Gender = y.Key.Gender,
                                ClassCount = y.SelectMany(z => z.cbox).Where(z => z != null).Select(z => z.ClassID).Distinct().Count(),
                                SubjectCount = y.SelectMany(z => z.sbox).Where(z => z != null).Select(z => z.SubjectID).Distinct().Count()
                            }).ToList();

                return View(list);
            }
        }

        public ActionResult StudentList()
        {
            using(AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Include("Enrollments").Select( x => new StudentViewModel
                {
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    EnrollmentCount = x.Enrollments.Select(y => y.ClassID).Distinct().Count()
                }).ToList();

                var list2 = (from s in db.Students
                             join e in db.Enrollments on s.StudentID equals e.StudentID into ebox
                             from eb in ebox.DefaultIfEmpty()
                             select new { s, ebox })
                            .GroupBy(x => x.s).Select(y => new StudentViewModel
                            {
                                Name = y.Key.FirstName + " " + y.Key.LastName,
                                Gender = y.Key.Gender,
                                EnrollmentCount = y.FirstOrDefault().ebox.Select(z => z.ClassID).Distinct().Count()
                            }).ToList();

                return View(list1);
            }
        }

        public ActionResult ClassList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Classes.Include("Teacher").Include("Subject").Include("Enrollments")
                    .Select(x => new ClassViewModel
                    {
                        Name = x.Name,
                        Subject = x.Subject.Name,
                        Teacher = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        EnrollmentsCount = x.Enrollments.Select(y => y.EnrollmentID).Distinct().Count()
                    }).ToList();

                var list2 = (from c in db.Classes
                             join t in db.Teachers on c.TeacherID equals t.TeacherID into tbox
                             from tb in tbox.DefaultIfEmpty()
                             join s in db.Subjects on c.SubjectID equals s.SubjectID into sbox
                             from sb in sbox.DefaultIfEmpty()
                             join e in db.Enrollments on c.ClassID equals e.ClassID into ebox
                             from eb in ebox.DefaultIfEmpty()
                             select new { c, tbox, sbox, ebox })
                            .GroupBy(x => x.c).Select(y => new ClassViewModel
                            {
                                Name = y.Key.Name,
                                Subject = y.FirstOrDefault().sbox.FirstOrDefault().Name,
                                Teacher = y.FirstOrDefault().tbox.FirstOrDefault().FirstName + " " + y.FirstOrDefault().tbox.FirstOrDefault().LastName,
                                EnrollmentsCount = y.FirstOrDefault().ebox.Select(z => z.EnrollmentID).Distinct().Count()
                            }).ToList();

                return View(list1);
            }
        }

        public ActionResult Test()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Select(x => new TestViewModel { Name = x.FirstName + " " + x.LastName, Number = x.Enrollments.Select(y => y.ClassID).Distinct().Count()}).ToList();            

                return View(list);
            } 
        }
    }
}