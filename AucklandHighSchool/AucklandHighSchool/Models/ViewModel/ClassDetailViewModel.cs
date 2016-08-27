using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class ClassDetailViewModel
    {
        public int ClassId { get; set; }

        [Display(Name = "Class Name:")]
        public String Name { get; set; }

        [Display(Name =  "Subject:")]
        public Subject SubjectProperty { get; set; }

        [Display(Name = "Teacher:")]
        public Teacher TeacherProperty { get; set; }

        [Display(Name = "Enrollment List:")]
        public List<Student> StudentList { get; set; }
    }
}