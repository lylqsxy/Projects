using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AucklandHighSchool.Models
{
    public class ClassDetailsViewModel
    {
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public List<Student> StudentList { get; set; }
    }
}