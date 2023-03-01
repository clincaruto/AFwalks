using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AutoMapper;

namespace AFwalks.API.Profiles
{
    public class RegionProfile: Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionVM>().ReverseMap();
        }
    }
}
