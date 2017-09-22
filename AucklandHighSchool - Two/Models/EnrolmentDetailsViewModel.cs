using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Models
{
    public class EnrolmentDetailsViewModel
    {
        public int Id { get; set; }
        public List<Class> SelectedClasses { get; set; }

        [DisplayName("Class")]
        public int SelectedClass { get; set; }

        public List<SelectListItem> ClassList { get; set; }
    }
}