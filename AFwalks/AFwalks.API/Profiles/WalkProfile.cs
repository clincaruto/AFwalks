using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AutoMapper;

namespace AFwalks.API.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile() 
        {
            CreateMap<Walk, WalkVM>().ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyVM>().ReverseMap();
        }
    }
}
