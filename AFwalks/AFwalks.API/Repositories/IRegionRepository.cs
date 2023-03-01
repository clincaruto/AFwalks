using AFwalks.API.Models.Domain;

namespace AFwalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
    }
}
