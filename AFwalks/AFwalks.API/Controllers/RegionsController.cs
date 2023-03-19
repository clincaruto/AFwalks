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
        public async Task<IActionResult> GetAllRegionsAsync() 
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
               // return BadRequest("Region not found");
            }

            var regionsDTO = mapper.Map<RegionVM>(region);

            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequestVM addRegionRequestVM)
        {
            // Request(DTO) to Domain model
            var region = new Region()
            {
                Code = addRegionRequestVM.Code,
                Area = addRegionRequestVM.Area,
                Lat = addRegionRequestVM.Lat,
                Long = addRegionRequestVM.Long,
                Name = addRegionRequestVM.Name,
                Population = addRegionRequestVM.Population,
            };
            // Pass details to Repository
            region = await regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new RegionVM
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };


           return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        //[ActionName("GetAllRegionsAsync")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from database
            var region =  await regionRepository.DeleteAsync(id);


            // If null Notfound
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new RegionVM
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };


            // return Ok Response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] UpdateRegionRequesrVM updateRegionRequesrVM)
        {
            // Convert DTO to Domain model
            var region = new Region()
            {
                Code = updateRegionRequesrVM.Code,
                Area = updateRegionRequesrVM.Area,
                Lat = updateRegionRequesrVM.Lat,
                Long = updateRegionRequesrVM.Long,
                Name = updateRegionRequesrVM.Name,
                Population = updateRegionRequesrVM.Population,
            };

            //Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);

            // if Null the NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDTO = new RegionVM
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return ok response
            return Ok (regionDTO);
        }

    }
}
