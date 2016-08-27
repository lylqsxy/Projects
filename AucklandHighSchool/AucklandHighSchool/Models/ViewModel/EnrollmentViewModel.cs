using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models.ViewModel
{
    public class EnrollmentViewModel
    {
        public int EnrollmentID { get; set; }
        public Class Class { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public int EnrollmentsCount { get; set; }
    }
}