using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class TeacherDetailViewModel
    {
        public int TeacherId { get; set; }

        [Display(Name = "Teacher Name:")]
        public String Name { get; set; }

        [Display(Name = "Gender:")]
        public String Gender { get; set; }

        [Display(Name = "Class List:")]
        public List<Class> ClassList { get; set; }

        [Display(Name = "Subject List:")]
        public List<Subject> SubjectList { get; set; }
    }
}