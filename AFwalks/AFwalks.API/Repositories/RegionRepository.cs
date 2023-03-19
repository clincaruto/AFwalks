using AFwalks.API.Data;
using AFwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private AFwalksDbContext context;

        public RegionRepository(AFwalksDbContext context) 
        {
            this.context = context;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region = await GetAsync(id);
            //if (region != null)
            //{
            //    context.Regions.Remove(region);
            //}
            //await context.SaveChangesAsync();
            //return region;
            if (region == null)
            {
                return null;
            }
            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await GetAsync(id);
            if (existingregion == null)
            {
                return null;
            }

           // we do not want to update the id, but want to update other properties
           existingregion.Code= region.Code;
           existingregion.Name= region.Name;
           existingregion.Area= region.Area;
           existingregion.Lat= region.Lat;
           existingregion.Long= region.Long;
           existingregion.Population= region.Population;

            await context.SaveChangesAsync();

            return existingregion; 


        }
    }
}
