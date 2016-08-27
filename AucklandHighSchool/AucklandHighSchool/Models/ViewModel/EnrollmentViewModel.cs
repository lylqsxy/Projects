using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AucklandHighSchool.Models.ViewModel
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Class ")]
        public List<Class> ClassList { get; set; }
    }
}