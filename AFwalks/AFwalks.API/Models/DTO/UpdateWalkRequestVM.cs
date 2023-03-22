namespace AFwalks.API.Models.DTO
{
    public class UpdateWalkRequestVM
    {
        // this is used so that the user do not add ID, Id will be automatically generated
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
