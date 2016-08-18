using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    public class Management
    {
        public static void AddInstructor()
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                Console.WriteLine("Input Last Name, First Name, Hire Date for the new instructor:");
                String LN = Console.ReadLine();
                String FN = Console.ReadLine();
                Nullable<System.DateTime> HD = DateTime.Parse(Console.ReadLine());
                Person p = new Person()
                {
                    LastName = LN,
                    FirstName = FN,
                    HireDate = HD,
                };


                List<Course> courses = db.Courses.ToList();
                foreach (var item in courses)
                {
                    Console.WriteLine("Course ID: {0}, Course Name: {1}", item.CourseID, item.Title);
                }

                Console.WriteLine("\r\nPlease choose the course, 0 for end:");
                List<int> selectedId = new List<int>();
                int input;
                do
                {
                    input = int.Parse(Console.ReadLine());
                    if (input != 0)
                    {
                        selectedId.Add(input);
                    }
                }
                while (input != 0);
                Console.WriteLine("Confirm the courses:");

                foreach (var id in selectedId)
                {
                    Console.WriteLine("Course ID: {0}", id.ToString());
                }
                string s = Console.ReadLine();
                if (s == "y")
                {
                    foreach (var id in selectedId)
                    {
                        var selectedCourse = db.Courses.Find(id);
                        p.Courses.Add(selectedCourse);
                    }
                    db.People.Add(p);
                    db.SaveChanges();
                    Console.WriteLine("All Done!");
                }
            }
        }

        public static void AddStudent()
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                Console.WriteLine("Input Last Name, First Name, Enrollment Date for the new student:");
                String LN = Console.ReadLine();
                String FN = Console.ReadLine();
                Nullable<System.DateTime> ED = DateTime.Parse(Console.ReadLine());
                Person p = new Person()
                {
                    LastName = LN,
                    FirstName = FN,
                    EnrollmentDate = ED
                };


                List<Course> courses = db.Courses.ToList();
                foreach (var item in courses)
                {
                    Console.WriteLine("Course ID: {0}, Course Name: {1}", item.CourseID, item.Title);
                }

                Console.WriteLine("\r\nPlease choose the course, 0 for end:");
                List<int> selectedId = new List<int>();
                int input;
                do
                {
                    input = int.Parse(Console.ReadLine());
                    if (input != 0)
                    {
                        selectedId.Add(input);
                    }
                }
                while (input != 0);
                Console.WriteLine("Confirm the courses:");

                foreach (var id in selectedId)
                {
                    Console.WriteLine("Course ID: {0}", id.ToString());
                }
                string s = Console.ReadLine();
                if (s == "y")
                {
                    foreach (var id in selectedId)
                    {
                        StudentGrade sg = new StudentGrade()
                        {
                            CourseID = id
                        };
                        p.StudentGrades.Add(sg);   
                    }
                    db.People.Add(p);
                    db.SaveChanges();
                    Console.WriteLine("All Done!");
                }
            }
        }

        public static void AddCourse()
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                Console.WriteLine("Input Title, Credits for the new course:");
                String T = Console.ReadLine();
                decimal C = decimal.Parse(Console.ReadLine());
                Course c = new Course()
                {
                    Title = T,
                    Credits = C
                };


                List<Department> departments = db.Departments.ToList();
                foreach (var item in departments)
                {
                    Console.WriteLine("Department ID: {0}, Department Name: {1}", item.DepartmentID, item.Name);
                }

                Console.WriteLine("\r\nPlease choose the department:");
                
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Confirm the department:");
                Console.WriteLine("Department ID: {0}", id.ToString());
                string s = Console.ReadLine();
                if (s == "y")
                {
                    c.DepartmentID = id;                      
                    db.Courses.Add(c);
                    db.SaveChanges();
                    Console.WriteLine("All Done!");
                }
            }
        }
        public static void ViewGrades()
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                List<Course> courses = db.Courses.ToList();
                foreach (var item in courses)
                {
                    Console.WriteLine("Course ID: {0}, Course Name: {1}", item.CourseID, item.Title);
                }

                Console.WriteLine("\r\nPlease choose the course:");

                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("The student grades of the course:");
                foreach(var item in db.StudentGrades.ToList())
                {
                    Console.WriteLine("Student Name: {0} {1}, Course Name: {2}, Grade: {3}", 
                        item.Person.FirstName, item.Person.LastName, item.Course.Title, item.Grade);
                }
                Console.WriteLine("All Done!");
            }
        }
        public static void ViewCourseInstructors()
        {
            using (SchoolManagementEntities db = new SchoolManagementEntities())
            {
                foreach (var item in db.Courses.ToList())
                {
                    Console.WriteLine("Course ID: {0}, Course Name: {1}\r\n================",
                        item.CourseID, item.Title);
                    foreach(var instructor in item.People.ToList())
                    {
                        Console.WriteLine("    Instructor ID: {0}, Instructor Name: {1} {2}",
                            instructor.PersonID, instructor.FirstName, instructor.LastName);
                    }
                    Console.WriteLine("\r\n");
                }
                Console.WriteLine("All Done!");
            }
        }
    }
}
