using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult SubjectList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
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
    }
}