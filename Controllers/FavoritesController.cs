using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045C_FinalProject.Data;
using IT3045C_FinalProject.Models;

namespace IT3045C_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FpDbContext _context;

        public FavoritesController(FpDbContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet("{studentId?}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FavoritesDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FavoritesDTO>>> GetFavorites(int? studentId = null)
        {
            if (studentId == null || studentId <= 0)
                return await _context.Favorites
                    .Select(x => new FavoritesDTO()
                    {
                        StudentId = x.StudentId,
                        FavoriteStore = x.FavoriteStore,
                        FavoriteAnimal = x.FavoriteAnimal,
                        FavoriteHobby = x.FavoriteHobby,
                        FavoriteSeason = x.FavoriteSeason
                    }).Take(5).ToArrayAsync();

            var favorites = await _context.Favorites.FindAsync(studentId);

            return favorites == null ? NotFound() : new FavoritesDTO[]
            {
                new FavoritesDTO()
                {
                StudentId = favorites.StudentId,
                FavoriteStore = favorites.FavoriteStore,
                FavoriteAnimal = favorites.FavoriteAnimal,
                FavoriteHobby = favorites.FavoriteHobby,
                FavoriteSeason = favorites.FavoriteSeason
                }
            };
        }

        // PUT: api/Favorites
        [HttpPut("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutFavorites(int studentId, FavoritesDTO favorites)
        {
            if (studentId != favorites.StudentId || !StudentsExists(favorites.StudentId))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(new Favorites()
            {
                StudentId = favorites.StudentId,
                FavoriteStore = favorites.FavoriteStore,
                FavoriteAnimal = favorites.FavoriteAnimal,
                FavoriteHobby = favorites.FavoriteHobby,
                FavoriteSeason = favorites.FavoriteSeason
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritesExists(studentId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Favorites
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FavoritesDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FavoritesDTO>> PostFavorites(FavoritesDTO favorites)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!StudentsExists(favorites.StudentId) || FavoritesExists(favorites.StudentId))
                return BadRequest();

            var newFavorites = new Favorites()
            {
                StudentId = favorites.StudentId,
                FavoriteStore = favorites.FavoriteStore,
                FavoriteAnimal = favorites.FavoriteAnimal,
                FavoriteHobby = favorites.FavoriteHobby,
                FavoriteSeason = favorites.FavoriteSeason
            };
            _context.Favorites.Add(newFavorites);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorites", new { studentId = favorites.StudentId }, favorites);
        }

        // DELETE: api/Favorites
        [HttpDelete("{studentId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFavorites(int studentId)
        {
            if (studentId <= 0)
                return BadRequest();
            var favorites = await _context.Favorites.FindAsync(studentId);
            if (favorites == null)
                return NotFound();

            _context.Favorites.Remove(favorites);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoritesExists(int studentId)
        {
            return _context.Favorites.Any(e => e.StudentId == studentId);
        }

        private bool StudentsExists(int studentId)
        {
            return _context.Student.Any(e => e.StudentId == studentId);
        }
    }
}
