using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPILab.Models;

namespace WebAPILab.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly B24162Lab6Context _context;

        public StudentController(B24162Lab6Context context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Student.ToListAsync();
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllNationality()
        {
            var nationalities = _context.Nationality.FromSqlRaw($"SelectNationality").AsEnumerable();
            return Ok(nationalities);
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllMajor()
        {
            var majors = _context.Major.FromSqlRaw($"SelectMajor").AsEnumerable();
            return Ok(majors);
        }

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                return _context.Student.ToList();
            } catch {
                throw;
            }

        }


        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllStudentsSP()
        {
            var students = _context.Student.FromSqlRaw($"SelectStudent").AsEnumerable();
            return Ok(students);
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            var result = _context.Database.ExecuteSqlRaw(
            "InsertUpdateStudent {0},{1},{2},{3},{4},{5}",
            student.StudentId,
            student.Name,
            student.Age,
            student.Nationality,
            student.Major,
            "Insert");

            if (result == 0)
            {
                return null;
            }
            return Ok(result);
        }




        // GET: api/Student/5
        [EnableCors("GetAllPolicy")]
        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
           var studentId = new SqlParameter("@StudentId",id);
            var student = _context.Student.
             FromSqlRaw($"SelectStudentByIdAPI @StudentId",studentId)
            .AsEnumerable().Single();
            

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPut]
        public IActionResult PutStudent(Student student)
        {

            var result = _context.Database.ExecuteSqlRaw("InsertUpdateStudent {0}, {1}, {2}, {3}, {4}, {5}",
                                student.StudentId,
                                student.Name,
                                student.Age,
                                student.Nationality,
                                student.Major,
                                "Update");
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]/{id}")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var result = _context.Database.ExecuteSqlRaw("DeleteStudent {0}", id);
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }

      

        //[Route("[action]")]
        //[HttpGet()]
        //public IActionResult Update(Student student)
        //{
        //    try
        //    {
        //        var studentId = new SqlParameter("@StudentId", student.StudentId);
        //        var name = new SqlParameter("@Name", student.Name);
        //        var age = new SqlParameter("@Age", student.Age);
        //        var nationality = new SqlParameter("@Nationality", student.Nationality);
        //        var major = new SqlParameter("@Major", student.Major);
        //        var action = new SqlParameter("@Action", "Update");

        //        var result = _context.Student.
        //         FromSqlRaw($"InsertUpdateStudent @StudentId,@Name,@Age,@Nationality,@Major,@Action",
        //         studentId, name, age, nationality, major, action);
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}



        // PUT: api/Student/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       

        //// DELETE: api/Student/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Student>> DeleteStudent(int id)
        //{
        //    var student = await _context.Student.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Student.Remove(student);
        //    await _context.SaveChangesAsync();

        //    return student;
        //}

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
