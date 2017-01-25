using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AucklandHighSchool.Models
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StaffCount { get; set; }
        public int ClassCount { get; set; }
        public int EnrolmentCount { get; set; }
    }
}
