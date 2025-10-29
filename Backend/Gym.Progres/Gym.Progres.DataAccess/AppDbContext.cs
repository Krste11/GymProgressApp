using Gym.Progres.Domain;
using Microsoft.EntityFrameworkCore;

namespace Gym.Progres.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    }
}
