using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class StudentDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Student Name:")]
        public String Name { get; set; }

        [Display(Name = "Gender:")]
        public String Gender { get; set; }

        [Display(Name = "Enrollment Class List:")]
        public List<Class> ClassList { get; set; }

    }
}