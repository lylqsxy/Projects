using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AucklandHighSchool.Models
{
    public class ClassEditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Class Name")]
        [Required(ErrorMessage = "Class name is required")]
        public string Name { get; set; }

        [DisplayName("Subject Name")]
        [Required(ErrorMessage = "Subject name is required")]
        public int SelectedSubject { get; set; }
        //public SelectListItem SelectedSubject { get; set; }

        [DisplayName("Teacher Name")]
        [Required(ErrorMessage = "Teacher name is required")]
        public int SelectedTeacher { get; set; }
        //public SelectListItem SelectedTeacher { get; set; }

        public List<SelectListItem> TeacherList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
    }

    
}