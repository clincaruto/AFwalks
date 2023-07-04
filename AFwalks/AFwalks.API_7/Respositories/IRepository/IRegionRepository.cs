using AFwalks.API_7.Models.Domain;

namespace AFwalks.API_7.Respositories.IRepository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region?> GetAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> DeleteAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region region);
    }
}
