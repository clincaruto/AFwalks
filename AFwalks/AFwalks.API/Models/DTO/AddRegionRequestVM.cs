namespace AFwalks.API.Models.DTO
{
    //this is used so that the user do not add ID, Id will be automatically generated
    public class AddRegionRequestVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
