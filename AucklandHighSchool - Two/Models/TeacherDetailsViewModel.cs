using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models
{
    public class TeacherDetailsViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public List<Subject> SubjectList { get; set; }
        public List<Class> ClassList { get; set; }
    }
}