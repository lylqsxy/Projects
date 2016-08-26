using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Gender")]
        public String Gender { get; set; }

        [Display(Name = "# Enrollment")]
        public int EnrollmentCount { get; set; }
    }
}