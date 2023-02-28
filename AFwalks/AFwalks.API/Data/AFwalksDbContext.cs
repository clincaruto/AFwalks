using AFwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API.Data
{
    public class AFwalksDbContext : DbContext
    {
        public AFwalksDbContext(DbContextOptions<AFwalksDbContext> options): base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
