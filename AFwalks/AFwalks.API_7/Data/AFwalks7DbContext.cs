using AFwalks.API_7.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API_7.Data
{
    public class AFwalks7DbContext : DbContext
    {
        public AFwalks7DbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
