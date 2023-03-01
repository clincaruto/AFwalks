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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }
    }
}
