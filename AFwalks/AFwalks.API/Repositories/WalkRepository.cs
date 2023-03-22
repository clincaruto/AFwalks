using AFwalks.API.Data;
using AFwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly AFwalksDbContext context;

        public WalkRepository(AFwalksDbContext context)
        {
            this.context = context;
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            // Assign New Id
            walk.Id = Guid.NewGuid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
           var walk = await GetAsync(id);

           if (walk != null)
           {
                context.Walks.Remove(walk);
                await context.SaveChangesAsync();
                return walk;
           }

            return null;

        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                context.Walks // we use this to add the navigation properties from the model of region and walkdifficulty
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await context.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await GetAsync(id);
            if (existingwalk != null)
            {
                // we do not want to update the id, but want to update other properties
                existingwalk.Length = walk.Length;
                existingwalk.Name = walk.Name;
                existingwalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingwalk.RegionId = walk.RegionId;
                await context.SaveChangesAsync();
                return existingwalk;

            }

            return null;


        }
    }
}
