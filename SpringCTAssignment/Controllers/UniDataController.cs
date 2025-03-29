using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Entities;
using Repository.Repo;
using Repository.ViewModels;
using SpringCTAssignment.Infrastructure;

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

        [HttpGet]
        public ActionResult<string> Login(string username, string pass)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass))
                {
                    return Unauthorized("You are not authorized");
                }
                if(username.Equals("SpringCtUser") && pass.Equals("ThisIsARandomPass"))
                {
                    var key = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString(Utility.SessionKeyName, key);
                    HttpContext.Session.SetString("Expiry", DateTime.Now.AddSeconds(Utility.SessionKeyAge).ToString());
                    return key;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Unauthorized("You are not authorized");
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudents(UserInput<List<Student>> data)
        {
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.key))
                {
                    return Unauthorized("You are not authorized");
                }

                var keyExpiry = DateTime.Now.AddDays(1);
                var keyexpiryStr = HttpContext.Session.GetString("Expiry");
                if (!string.IsNullOrWhiteSpace(keyexpiryStr))
                {
                    keyExpiry = Convert.ToDateTime(keyexpiryStr);
                }

                var activeKey = HttpContext.Session.GetString(Utility.SessionKeyName) ?? string.Empty;

                if(keyExpiry >=  DateTime.Now && activeKey != data.key)
                {
                    return Unauthorized("You are not authorized or your session has expired, please try again!");
                }

                var students = data.Data;
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
        public async Task<ActionResult<List<Student>>> GetStudents(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return Unauthorized("You are not authorized");
                }

                var keyExpiry = DateTime.Now.AddDays(1);
                var keyexpiryStr = HttpContext.Session.GetString("Expiry");
                if (!string.IsNullOrWhiteSpace(keyexpiryStr))
                {
                    keyExpiry = Convert.ToDateTime(keyexpiryStr);
                }

                var activeKey = HttpContext.Session.GetString(Utility.SessionKeyName) ?? string.Empty;

                if (keyExpiry >= DateTime.Now && activeKey != key)
                {
                    return Unauthorized("You are not authorized or your session has expired, please try again!");
                }

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

        [HttpPost]
        public async Task<ActionResult<List<Student>>> GetStudent(UserInput<string> data)
        {
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.key))
                {
                    return Unauthorized("You are not authorized");
                }

                var keyExpiry = DateTime.Now.AddDays(1);
                var keyexpiryStr = HttpContext.Session.GetString("Expiry");
                if (!string.IsNullOrWhiteSpace(keyexpiryStr))
                {
                    keyExpiry = Convert.ToDateTime(keyexpiryStr);
                }

                var activeKey = HttpContext.Session.GetString(Utility.SessionKeyName) ?? string.Empty;

                if (keyExpiry >= DateTime.Now && activeKey != data.key)
                {
                    return Unauthorized("You are not authorized or your session has expired, please try again!");
                }

                var studentID = data.Data;
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
        public async Task<ActionResult<List<Course>>> GetCourses(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return Unauthorized("You are not authorized");
                }

                var keyExpiry = DateTime.Now.AddDays(1);
                var keyexpiryStr = HttpContext.Session.GetString("Expiry");
                if (!string.IsNullOrWhiteSpace(keyexpiryStr))
                {
                    keyExpiry = Convert.ToDateTime(keyexpiryStr);
                }

                var activeKey = HttpContext.Session.GetString(Utility.SessionKeyName) ?? string.Empty;

                if (keyExpiry >= DateTime.Now && activeKey != key)
                {
                    return Unauthorized("You are not authorized or your session has expired, please try again!");
                }

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
        public async Task<ActionResult> SetupStudentCourseRel(UserInput<List<StudentCourseRel>> data)
        {
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.key))
                {
                    return Unauthorized("You are not authorized");
                }

                var keyExpiry = DateTime.Now.AddDays(1);
                var keyexpiryStr = HttpContext.Session.GetString("Expiry");
                if (!string.IsNullOrWhiteSpace(keyexpiryStr))
                {
                    keyExpiry = Convert.ToDateTime(keyexpiryStr);
                }

                var activeKey = HttpContext.Session.GetString(Utility.SessionKeyName) ?? string.Empty;

                if (keyExpiry >= DateTime.Now && activeKey != data.key)
                {
                    return Unauthorized("You are not authorized or your session has expired, please try again!");
                }

                var rels = data.Data;

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
