using AFwalks.API_7.Data;
using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Respositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API_7.Respositories.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AFwalks7DbContext context;

        public RegionRepository(AFwalks7DbContext context)
        {
            this.context = context;
        }
        public async Task<Region> CreateAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public Task<Region> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await GetAsync(id);
            if (existingregion == null)
            {
                return null;
            }

            // we do not want to update the id, but want to update other properties
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.RegionImageUrl= region.RegionImageUrl;

            await context.SaveChangesAsync();

            return existingregion;
        }
    }
}
