using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public interface IRepo
    {
        Task<int> AddUpdateStudent(Student student);
        Task<int> AddUpdateStudents(List<Student> students);
        Task<int> DeleteStudent(Student student);
        IQueryable<Student> GetStudents();

        Task<int> AddUpdateCourse(Course course);
        Task<int> AddUpdateCourses(List<Course> courses);
        Task<int> DeleteCourse(Course course);
        IQueryable<Course> GetCourses();

        Task<int> AddUpdateStudentCourseRel(StudentCourseRel studentCourseRel);
        Task<int> AddUpdateStudentCourseRels(List<StudentCourseRel> studentCourseRel);
        Task<int> DeleteStudentCourseRel(StudentCourseRel studentCourseRel);
        IQueryable<StudentCourseRel> GetStudentCourseRels();

        Task<List<StudentCourseRel>> GetStudentsWithCourses();
    }
}
