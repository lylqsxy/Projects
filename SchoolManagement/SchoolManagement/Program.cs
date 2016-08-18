using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            String s = string.Empty;
            s += "1. Create new Instructor and assign instructor to the Course.\r\n";
            s += "2. Create new Student and enroll to the Course.\r\n";
            s += "3. Create new Course and assign to Department\r\n";
            s += "4. View all Student grades of Course.\r\n";
            s += "5. View all Course and Instructors.\r\n";
            Console.WriteLine(s);
            Console.WriteLine("Please choose: ");
            String c = Console.ReadLine();
            switch(c)
            {
                case "1":
                    Management.AddInstructor();
                    break;
                case "2":
                    Management.AddStudent();
                    break;
                case "3":
                    Management.AddCourse();
                    break;
                case "4":
                    Management.ViewGrades();
                    break;
                case "5":
                    Management.ViewCourseInstructors();
                    break;
            }
            Console.Read();
        }
    }
}
