using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models
{
    public class SubjectDetailsViewModel
    {
        public string Name { get; set; }
        public List<Teacher> TeacherList { get; set; }
        public List<Class> ClassList { get; set; }
        public List<Student> StudentList { get; set; }
    }
}