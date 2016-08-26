using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucklandHighSchool.Models.ViewModel;
using AucklandHighSchool.Models;

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
                    Id = x.TeacherID,
                    Name = x.FirstName + " " + x.LastName,
                    Gender = x.Gender,
                    ClassCount = x.Classes.Select(y => y.ClassID).Distinct().Count(),
                    SubjectCount = x.Classes.Select(y => y.SubjectID).Distinct().Count()
                }).ToList();

                return View(list);
            }
        }
    }
}