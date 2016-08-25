using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
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
    }
}