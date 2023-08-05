using Microsoft.EntityFrameworkCore;
using IT3045C_FinalProject.Models;

namespace IT3045C_FinalProject.Data
{
    public class FpDbContext : DbContext
    {
        public FpDbContext(DbContextOptions<FpDbContext> options) : base(options) { }

        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Culture> Culture { get; set; } = default!;
        public DbSet<Favorites> Favorites { get; set; } = default!;
        public DbSet<Opinions> Opinions { get; set; } = default!;
    }
}