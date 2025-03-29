using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModels
{
    public class StudentCourseRelVM : Student
    {
        public List<Course> StudentCourses { get; set; }
        public string CourseEnrolledStr { get; set; }
    }
}
