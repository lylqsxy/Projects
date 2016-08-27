using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class ClassViewModel
    {
        [Display(Name = "Class ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Subject")]
        public String Subject { get; set; }

        [Display(Name = "Teacher")]
        public String Teacher { get; set; }

        [Display(Name = "# Enrollments")]
        public int EnrollmentsCount { get; set; }
    }
}