using AFwalks.API.Data;
using AFwalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly AFwalksDbContext context;

        public WalkDifficultyRepository(AFwalksDbContext context)
        {
            this.context = context;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            // Assign New ID
            walkDifficulty.Id= Guid.NewGuid();
            await context.WalkDifficulty.AddAsync(walkDifficulty);
            await context.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDiff = await GetAsync(id);

            if (walkDiff != null)
            { 
                context.WalkDifficulty.Remove(walkDiff);
                await context.SaveChangesAsync();
                return walkDiff;
            }

            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await context.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDiff = await GetAsync(id);
            if (existingWalkDiff == null)
            {
                return null;
            }

            // we do not want to update the id, but want to update other properties
            existingWalkDiff.Code = walkDifficulty.Code;
            await context.SaveChangesAsync();
            return existingWalkDiff;
            
        }
    }
}
