using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public class Repo : IRepo
    {
        internal DbaseContext _ctx;
        public Repo(DbaseContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<int> AddUpdateStudent(Student student)
        {
            try
            {
                if (student != null)
                {
                    if (student.ID > 0)
                    {
                        _ctx.Entry(student).State = EntityState.Modified;
                    }
                    else
                    {
                        _ctx.Add(student);
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public async Task<int> AddUpdateStudents(List<Student> students)
        {
            try
            {
                if (students != null && students.Count > 0)
                {
                    foreach (var student in students)
                    {
                        if (student.ID > 0)
                        {
                            _ctx.Entry(student).State = EntityState.Modified;
                        }
                        else
                        {
                            _ctx.Add(student);
                        }
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public async Task<int> DeleteStudent(Student student)
        {
            try
            {
                if (student != null && student.ID > 0)
                {
                    student.IsInactive = true;
                    _ctx.Entry(student).State = EntityState.Modified;
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public IQueryable<Student> GetStudents()
        {
            try
            {
                return _ctx.Students.Where(x => x.IsInactive == false).AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddUpdateCourse(Course course)
        {
            try
            {
                if(course is not null)
                {
                    if (course.ID > 0)
                    {
                        _ctx.Entry(course).State = EntityState.Modified;
                    }
                    else
                    {
                        _ctx.Add(course);
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        public async Task<int> AddUpdateCourses(List<Course> courses)
        {
            try
            {
                if (courses is not null && courses.Count() > 0)
                {
                    foreach (var course in courses)
                    {
                        if (course.ID > 0)
                        {
                            _ctx.Entry(course).State = EntityState.Modified;
                        }
                        else
                        {
                            _ctx.Add(course);
                        }
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public async Task<int> DeleteCourse(Course course)
        {
            try
            {
                if (course is not null && course.ID > 0)
                {
                    course.IsInactive = true;
                    _ctx.Entry(course).State = EntityState.Modified;
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public IQueryable<Course> GetCourses()
        {
            try
            {
                return _ctx.Courses.Where(x => x.IsInactive == false).AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddUpdateStudentCourseRel(StudentCourseRel studentCourseRel)
        {
            try
            {
                if (studentCourseRel is not null)
                {
                    if (studentCourseRel.ID > 0)
                    {
                        _ctx.Entry(studentCourseRel).State = EntityState.Modified;
                    }
                    else
                    {
                        _ctx.Add(studentCourseRel);
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public async Task<int> AddUpdateStudentCourseRels(List<StudentCourseRel> studentCourseRel)
        {
            try
            {
                if (studentCourseRel is not null && studentCourseRel.Count > 0)
                {
                    foreach (var rel in studentCourseRel)
                    {
                        if (rel.ID > 0)
                        {
                            _ctx.Entry(rel).State = EntityState.Modified;
                        }
                        else
                        {
                            _ctx.Add(rel);
                        }
                    }
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        public async Task<int> DeleteStudentCourseRel(StudentCourseRel studentCourseRel)
        {
            try
            {
                if(studentCourseRel is not null && studentCourseRel.ID > 0)
                {
                    studentCourseRel.IsInactive = true;
                    _ctx.Entry(studentCourseRel).State = EntityState.Modified;
                    return await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        public IQueryable<StudentCourseRel> GetStudentCourseRels()
        {
            try
            {
                return _ctx.StudentCourseRels.Where(x => x.IsInactive == false).AsNoTracking().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<StudentCourseRel>> GetStudentsWithCourses()
        {
            try
            {
                var query = from a in _ctx.Students
                            join b in _ctx.StudentCourseRels on a.ID equals b.StudentID
                            join c in _ctx.Courses on b.CourseID equals c.ID
                            select new StudentCourseRel
                            {
                                ID = a.ID,
                                StudentID = a.ID,
                                Student = a,
                                CourseID = c.ID,
                                Course = c
                            };
                var result = await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return new List<StudentCourseRel>();
        }
    }
}
