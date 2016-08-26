using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class SubjectDetailViewModel
    {
        [Display(Name = "Subject Name:")]
        public String Name { get; set; }

        [Display(Name = "Class List:")]
        public List<String> ClassList { get; set; }

        [Display(Name = "Teacher List:")]
        public List<String> TeacherList { get; set; }

        [Display(Name = "Enrollment List:")]
        public List<String> StudentList { get; set; }
    }
}