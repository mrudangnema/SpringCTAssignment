using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Repo;
using Repository.ViewModels;

namespace SpringCTAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UniDataController : ControllerBase
    {
        internal IRepo _repo;
        public UniDataController(IRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudents(List<Student> students)
        {
            try
            {
                if (students is not null && students.Count > 0)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest("Invalid student data");
                    }

                    foreach (var student in students)
                    {
                        //Set all IDs to 0 as this is a create API
                        student.ID = 0;
                        student.StudentID = string.Format("{0}{1}", student.FirstName.Substring(0, 1), student.LastName);
                        student.AddedOn = DateTime.Now;
                    }

                    var result = await _repo.AddUpdateStudents(students);
                    if (result > 0)
                    {
                        return Ok(students);
                    }
                    else
                    {
                        return BadRequest("Failed to add students");
                    }
                }
                else
                {
                    return BadRequest("No student data found");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            try
            {
                //var students = await Task.Run(() => _repo.GetStudents().ToList());
                var studentsWithCourses = await _repo.GetStudentsWithCourses();
                if (studentsWithCourses is null || studentsWithCourses.Count == 0)
                {
                    return NotFound("No student data found");
                }

                var students = studentsWithCourses.GroupBy(m => m.ID).Select(m =>
                    new StudentCourseRelVM
                    {
                        ID = m.Key,
                        FirstName = m.First().Student.FirstName,
                        LastName = m.First().Student.LastName,
                        StudentID = m.First().Student.StudentID,
                        AddedOn = m.First().AddedOn,
                        StudentCourses = m.Select(n => n.Course).ToList(),
                        CourseEnrolledStr = String.Join(", ", m.Select(k => k.Course.Name))
                    }
                ).ToList();
                if (students is not null && students.Count > 0)
                {
                    return Ok(students);
                }
                else
                {
                    return NotFound("No student data found");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent(string studentID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(studentID))
                {
                    return BadRequest("Invalid student data");
                }
                var student = await Task.Run(() => _repo.GetStudents().Where(m => m.StudentID == studentID).FirstOrDefault());
                if (student is not null)
                {
                    return Ok(student);
                }
                else
                {
                    return NotFound("No student data found");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            try
            {
                var courses = await Task.Run(() => _repo.GetCourses().ToList());
                if (courses is not null)
                {
                    return Ok(courses);
                }
                else
                {
                    return NotFound("No course data found");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SetupStudentCourseRel(List<StudentCourseRel> rels)
        {
            try
            {
                if (rels is null || rels.Count == 0)
                {
                    return BadRequest("Invalid student course data");
                }
                foreach (var rel in rels)
                {
                    rel.ID = 0;
                    rel.AddedOn = DateTime.Now;
                }
                var result = await _repo.AddUpdateStudentCourseRels(rels);
                if(result > 0)
                {
                    return Ok("Student and course relationships have been saved!");
                }
                return BadRequest("Failed to save student and course relationships");
            }
            catch (Exception)
            {
                throw;
            }
        }
   }
}
