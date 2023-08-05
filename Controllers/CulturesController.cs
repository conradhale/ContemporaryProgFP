using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_FinalProject.Data;
using IT3045C_FinalProject.Models;

namespace IT3045C_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CulturesController : ControllerBase
    {
        private readonly FpDbContext _context;

        public CulturesController(FpDbContext context)
        {
            _context = context;
        }

        // GET: api/Cultures
        [HttpGet("{studentId?}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CultureDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CultureDTO>>> GetCultures(int? studentId = null)
        {
            if (studentId == null || studentId <= 0)
                return await _context.Culture
                    .Select(x => new CultureDTO()
                    {
                        StudentId = x.StudentId,
                        BirthCountry = x.BirthCountry,
                        Nationality = x.Nationality,
                        FirstLanguage = x.Nationality,
                        PrimaryLanguage = x.PrimaryLanguage
                    }).Take(5).ToArrayAsync();

            var culture = await _context.Culture.FindAsync(studentId);

            return culture == null ? NotFound() : new CultureDTO[]
            {
                new CultureDTO()
                {
                    StudentId = culture.StudentId,
                    BirthCountry = culture.BirthCountry,
                    Nationality = culture.Nationality,
                    FirstLanguage = culture.Nationality,
                    PrimaryLanguage = culture.PrimaryLanguage
                }
            };
        }

        // PUT: api/Cultures
        [HttpPut("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCultures(int studentId, CultureDTO culture)
        {
            if (studentId != culture.StudentId || !StudentsExists(culture.StudentId))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(new Culture()
            {
                StudentId = culture.StudentId,
                BirthCountry = culture.BirthCountry,
                Nationality = culture.Nationality,
                FirstLanguage = culture.Nationality,
                PrimaryLanguage = culture.PrimaryLanguage
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CulturesExists(studentId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Cultures
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CultureDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CultureDTO>> PostCultures(CultureDTO culture)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!StudentsExists(culture.StudentId) || CulturesExists(culture.StudentId))
                return BadRequest();

            _context.Culture.Add(new Culture()
            {
                StudentId = culture.StudentId,
                BirthCountry = culture.BirthCountry,
                Nationality = culture.Nationality,
                FirstLanguage = culture.Nationality,
                PrimaryLanguage = culture.PrimaryLanguage
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCultures", new { studentId = culture.StudentId }, culture);
        }

        // DELETE: api/Cultures
        [HttpDelete("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCultures(int studentId)
        {
            if (studentId <= 0)
                return BadRequest();
            var culture = await _context.Culture.FindAsync(studentId);
            if (culture == null)
                return NotFound();

            _context.Culture.Remove(culture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CulturesExists(int studentId)
        {
            return _context.Culture.Any(e => e.StudentId == studentId);
        }

        private bool StudentsExists(int studentId)
        {
            return _context.Student.Any(e => e.StudentId == studentId);
        }
    }
}
