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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed Data for Difficulties
            // Easy, Medium , Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("6cbc474b-546a-462f-80ae-62b15a08210c"),
                    Name = "Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("21663eb5-77eb-485d-8f42-63ae94c43cb9"),
                    Name = "Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("20b3c9c4-366c-4572-bba0-e0702125fefd"),
                    Name = "Hard"
                }

            };

            // Seed Difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("e4c5d0b8-2a2f-48de-9623-9d808a8bd9cf"),
                    Code = "NGA",
                    Name= "Chuks Region",
                    RegionImageUrl="https://wallpapercave.com/w/d8zqgVd"

                },
                new Region()
                {
                    Id = Guid.Parse("5f354405-b144-47ba-abaf-f160b2861de6"),
                    Code = "CND",
                    Name = "Sagemode",
                    RegionImageUrl = "https://wallpapercave.com/w/d8zqgVd"
                },
                new Region()
                {
                    Id = Guid.Parse("d267bd9e-8cda-4cac-8188-e3872c7cf877"),
                    Code = "SGA",
                    Name = "Boss Nation",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("7a19d5d5-cd6b-4cf5-be96-d193043b4e5a"),
                    Code = "KEN",
                    Name = "Naruto",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("3c41788e-abfd-44e1-9750-32f27f64fd3a"),
                    Code = "STL",
                    Name = "Southland",
                    RegionImageUrl = null
                }
            };

            // Seed regions to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
