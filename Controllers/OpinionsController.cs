using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_FinalProject.Data;
using IT3045C_FinalProject.Models;

namespace IT3045C_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionsController : ControllerBase
    {
        private readonly FpDbContext _context;

        public OpinionsController(FpDbContext context)
        {
            _context = context;
        }

        // GET: api/Opinions
        [HttpGet("{studentId?}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OpinionsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OpinionsDTO>>> GetOpinions(int? studentId = null)
        {
            if (studentId == null || studentId <= 0)
                return await _context.Opinions
                    .Select(x => new OpinionsDTO()
                    {
                        StudentId = x.StudentId,
                        CatsOverDogs = x.CatsOverDogs,
                        IOSOverAdroid = x.IOSOverAdroid,
                        PlaystationOverXbox = x.PlaystationOverXbox,
                        PineappleOnPizza = x.PineappleOnPizza
                    }).Take(5).ToArrayAsync();

            var opinions = await _context.Opinions.FindAsync(studentId);

            return opinions == null ? NotFound() : new OpinionsDTO[]
            {
                new OpinionsDTO()
                {
                    StudentId = opinions.StudentId,
                    CatsOverDogs = opinions.CatsOverDogs,
                    IOSOverAdroid = opinions.IOSOverAdroid,
                    PlaystationOverXbox = opinions.PlaystationOverXbox,
                    PineappleOnPizza = opinions.PineappleOnPizza
                }
            };
        }

        // PUT: api/Opinions
        [HttpPut("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOpinions(int studentId, OpinionsDTO opinions)
        {
            if (studentId != opinions.StudentId || !StudentsExists(opinions.StudentId))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(new Opinions()
            {
                StudentId = opinions.StudentId,
                CatsOverDogs = opinions.CatsOverDogs,
                IOSOverAdroid = opinions.IOSOverAdroid,
                PlaystationOverXbox = opinions.PlaystationOverXbox,
                PineappleOnPizza = opinions.PineappleOnPizza
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpinionsExists(studentId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Opinions
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OpinionsDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OpinionsDTO>> PostOpinions(OpinionsDTO opinions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!StudentsExists(opinions.StudentId) || OpinionsExists(opinions.StudentId))
                return BadRequest();

            var newOpinions = new Opinions()
            {
                StudentId = opinions.StudentId,
                CatsOverDogs = opinions.CatsOverDogs,
                IOSOverAdroid = opinions.IOSOverAdroid,
                PlaystationOverXbox = opinions.PlaystationOverXbox,
                PineappleOnPizza = opinions.PineappleOnPizza
            };
            _context.Opinions.Add(newOpinions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpinions", new { studentId = opinions.StudentId }, opinions);
        }

        // DELETE: api/Opinions
        [HttpDelete("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOpinions(int studentId)
        {
            if (studentId <= 0)
                return BadRequest();
            var opinions = await _context.Opinions.FindAsync(studentId);
            if (opinions == null)
                return NotFound();

            _context.Opinions.Remove(opinions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpinionsExists(int studentId)
        {
            return _context.Opinions.Any(e => e.StudentId == studentId);
        }

        private bool StudentsExists(int studentId)
        {
            return _context.Student.Any(e => e.StudentId == studentId);
        }
    }
}
