using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Models.DTO;
using AutoMapper;

namespace AFwalks.API_7.Configuration.MappingsProfile
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionReguestDto, Region>().ReverseMap();

            // Walks
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            // This is for mapping when the mehthods are different from the DTO and Domain model
            CreateMap<UserDTO, UserDomain>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
                .ReverseMap();
        }
    }

    public class UserDTO
    {
        public string FullName { get; set; }
    }

    public class UserDomain
    {
        public string Name { get; set; }
    }
}
