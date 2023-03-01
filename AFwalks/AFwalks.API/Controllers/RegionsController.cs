using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AFwalks.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        private IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        //public IActionResult GetAllRegions()
        //{
        //    var regions = new List<Region>()
        //    {
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Wellington",
        //            Code = "WLG",
        //            Area = 227755,
        //            Lat = -1.8822,
        //            Long = 299.88,
        //            Population = 500000
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Auckland",
        //            Code = "WLG",
        //            Area = 227755,
        //            Lat = -1.8822,
        //            Long = 299.88,
        //            Population = 500000
        //        },
        //    };
            
        //    return Ok(regions);
        //}
        public async Task<IActionResult> GetAllRegions() 
        {
            var regions = await regionRepository.GetAllAsync();

            // return DTO regions
            //var regionsDTO = new List<Models.DTO.RegionVM>();
            //regions.ToList().ForEach(regionDOM =>
            //{
            //    var regionDTO = new Models.DTO.RegionVM()
            //    {
            //        Id = regionDOM.Id,
            //        Code= regionDOM.Code,
            //        Name = regionDOM.Name,
            //        Area = regionDOM.Area,
            //        Lat= regionDOM.Lat,
            //        Long= regionDOM.Long,
            //        Population  =regionDOM.Population,

            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<RegionVM>>(regions);
            return Ok(regionsDTO);
        }
    }
}
