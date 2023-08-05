using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_FinalProject.Data;
using IT3045C_FinalProject.Models;

namespace IT3045C_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly FpDbContext _context;

        public StudentsController(FpDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet("{studentId?}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents(int? studentId = null)
        {
            if (studentId == null || studentId <= 0)
                return await _context.Student
                    .Select(x => new StudentDTO()
                    {
                        StudentId = x.StudentId,
                        BirthDate = x.BirthDate,
                        FullName = x.FullName,
                        Program = x.Program,
                        Year = x.Year
                    }).Take(5).ToArrayAsync();

            var student = await _context.Student.FindAsync(studentId);

            return student == null ? NotFound() : new StudentDTO[]
            {
                new StudentDTO()
                {
                    StudentId = student.StudentId,
                    BirthDate = student.BirthDate,
                    FullName = student.FullName,
                    Program = student.Program,
                    Year = student.Year
                }
            };
        }

        // PUT: api/Students
        [HttpPut("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStudents(int studentId, StudentDTO student)
        {
            if (studentId != student.StudentId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(new Student()
            {
                StudentId = student.StudentId,
                BirthDate = student.BirthDate,
                FullName = student.FullName,
                Program = student.Program,
                Year = student.Year
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(studentId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PostStudents(NewStudentDTO student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newStudent = new Student()
            {
                BirthDate = student.BirthDate,
                FullName = student.FullName,
                Program = student.Program,
                Year = student.Year
            };

            _context.Student.Add(newStudent);
            await _context.SaveChangesAsync();

            var response = new StudentDTO()
            {
                StudentId = newStudent.StudentId,
                BirthDate = newStudent.BirthDate,
                FullName = newStudent.FullName,
                Program = newStudent.Program,
                Year = newStudent.Year
            };
            return CreatedAtAction("GetStudents", new { studentId = response.StudentId }, response);
        }

        // DELETE: api/Students
        [HttpDelete("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStudents(int studentId)
        {
            if (studentId <= 0)
                return BadRequest();
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
                return NotFound();

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentsExists(int studentId)
        {
            return _context.Student.Any(e => e.StudentId == studentId);
        }
    }
}
