using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class SubjectViewModel
    {
        [Display(Name = "Subject ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "# Staff")]
        public int TeacherCount { get; set; }

        [Display(Name = "# Class")]
        public int ClassCount { get; set; }

        [Display(Name = "# Enrollments")]
        public int EnrollmentsCount { get; set; }
    }
}