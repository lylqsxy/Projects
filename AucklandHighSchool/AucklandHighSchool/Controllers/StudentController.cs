using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;

namespace AucklandHighSchool.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentList()
        {
            using (AucklandHighSchoolEntities db = new AucklandHighSchoolEntities())
            {
                var list = db.Students.Include("Enrollments").Select(x => new StudentViewModel
                {
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    EnrollmentCount = x.Enrollments.Select(y => y.EnrollmentID).Distinct().Count()
                }).ToList();

                var list2 = (from s in db.Students
                             join e in db.Enrollments on s.StudentID equals e.StudentID into ebox
                             from eb in ebox.DefaultIfEmpty()
                             select new { s, ebox })
                            .GroupBy(x => x.s).Select(y => new StudentViewModel
                            {
                                Name = y.Key.FirstName + " " + y.Key.LastName,
                                Gender = y.Key.Gender,
                                EnrollmentCount = y.FirstOrDefault().ebox.Select(z => z.EnrollmentID).Distinct().Count()
                            }).ToList();

                return View(list);
            }
        }
    }
}