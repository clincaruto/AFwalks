//using AFwalks.API.Models.Domain;

namespace AFwalks.API.Models.DTO
{
    public class WalkVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation Properties
        public RegionVM Region { get; set; }
        public WalkDifficultyVM WalkDifficulty { get; set; }
    }
}
