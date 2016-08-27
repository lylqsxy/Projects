using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models.ViewModel
{
    public class EnrollmentListViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<EnrollmentViewModel> Evms { get; set; }
    }
}