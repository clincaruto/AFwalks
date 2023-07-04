using AFwalks.API_7.Data;
using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Respositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API_7.Respositories.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly AFwalks7DbContext context;

        public WalkRepository(AFwalks7DbContext context)
        {
            this.context = context;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await GetAsync(id);
            if (walk == null)
            {
                return null;
            }

            context.Walks.Remove(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var walks = context.Walks.Include(x => x.Region).Include(x => x.Difficulty).AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;  

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            

            //return await context.Walks
            //    .Include(x => x.Region)
            //    .Include(x => x.Difficulty)
            //    .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await context.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await GetAsync(id);
            if (existingwalk != null)
            {
                // we do not want to update the id, but want to update other properties
                existingwalk.Name = walk.Name;
                existingwalk.Description = walk.Description;
                existingwalk.LengthInKm = walk.LengthInKm;
                existingwalk.WalkImageUrl= walk.WalkImageUrl;
                existingwalk.DifficultyId= walk.DifficultyId;
                existingwalk.RegionId= walk.RegionId;
                await context.SaveChangesAsync();
                return existingwalk;
            }

            return null;
        }
    }
}
