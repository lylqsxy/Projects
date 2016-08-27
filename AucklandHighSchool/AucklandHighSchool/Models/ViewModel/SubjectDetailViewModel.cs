using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class SubjectDetailViewModel
    {
        public int SubjectId { get; set; }

        [Display(Name = "Subject Name:")]
        public String Name { get; set; }

        [Display(Name = "Class List:")]
        public List<Class> ClassList { get; set; }

        [Display(Name = "Teacher List:")]
        public List<Teacher> TeacherList { get; set; }

        [Display(Name = "Enrollment List:")]
        public List<Student> StudentList { get; set; }
    }
}