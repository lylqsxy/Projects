using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int EnrolmentCount { get; set; }
    }
}