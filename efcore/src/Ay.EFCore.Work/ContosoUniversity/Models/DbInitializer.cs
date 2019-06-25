using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public static class DbInitializer
    {
        public static void Initializer(SchoolContext context){
            if(context.Student.Any())
            {
                return;
            }
            var students = new Student[]{
                new Student{ FirstMidName="张三", LastName="丰", EnrollmentDate=DateTime.Parse("2019-06-01")},
                new Student{ FirstMidName="黄药", LastName="师", EnrollmentDate=DateTime.Parse("2019-06-12")},
                new Student{ FirstMidName="洪七", LastName="公", EnrollmentDate=DateTime.Parse("2019-02-24")}
            };
            foreach(Student s in students)
            {
                context.Student.Add(s);
            }
            context.SaveChanges();

            var couses = new Course[]{
                new Course{ CourseID=1111, Title="语文", Credits=3},
                new Course{ CourseID=2222, Title="数学", Credits=3},
                new Course{ CourseID=3333, Title="英语", Credits=3}
            };

            foreach(Course c in couses)
            {
                context.Course.Add(c);
            }
            context.SaveChanges();


            var enrollments = new Enrollment[]{
                new Enrollment{ StudentID=1,CourseID=1111, Grade=Grade.A},
                new Enrollment{ StudentID=1,CourseID=3333, Grade=Grade.B},

                new Enrollment{ StudentID=2,CourseID=2222, Grade=Grade.C},
                new Enrollment{ StudentID=2,CourseID=3333, Grade=Grade.D},
                new Enrollment{ StudentID=2,CourseID=1111, Grade=Grade.A},

                new Enrollment{ StudentID=3,CourseID=2222, Grade=Grade.A},
                new Enrollment{ StudentID=3,CourseID=3333, Grade=Grade.F},

                //new Enrollment{ StudentID=4,CourseID=1111, Grade=Grade.B},
                //new Enrollment{ StudentID=5,CourseID=3333, Grade=Grade.C},
                //new Enrollment{ StudentID=6,CourseID=3333, Grade=Grade.A}
            };

            foreach(Enrollment e in enrollments){
                context.Enrollment.Add(e);
            }
            context.SaveChanges();
        }
    }
}
