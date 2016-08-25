using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
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

                return View(list);
            }
        }
    }
}