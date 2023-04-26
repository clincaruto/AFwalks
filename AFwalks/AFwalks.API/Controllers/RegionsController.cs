using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AFwalks.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "reader")]
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
		[Authorize(Roles = "reader")]
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
		[Authorize(Roles = "writer")]
		public async Task<IActionResult> AddRegionAsync(AddRegionRequestVM addRegionRequestVM)
        {
            // Validate the Request
            //var Validate = ValidateAddRegionAsync(addRegionRequestVM);
            //if (!Validate)
            //{
            //    return BadRequest(ModelState);
            //}

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
		[Authorize(Roles = "writer")]
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
		[Authorize(Roles = "writer")]

		public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] UpdateRegionRequesrVM updateRegionRequesrVM)
        {
            // Validate the Request
            //var Validate = ValidateUpdateRegionAsync(updateRegionRequesrVM);
            //if (!Validate)
            //{
            //    return BadRequest(ModelState);
            //}


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

        #region MyRegion

        private bool ValidateAddRegionAsync(AddRegionRequestVM addRegionRequestVM)
        {
            if (addRegionRequestVM == null)
            {
                ModelState.AddModelError(nameof(addRegionRequestVM), $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequestVM.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Code), 
                    $"{nameof(addRegionRequestVM.Code)} cannot be null or empty or white space");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequestVM.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Name),
                    $"{nameof(addRegionRequestVM.Name)} cannot be null or empty or white space");
            }

            if (addRegionRequestVM.Area <=0)
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Area),
                    $"{nameof(addRegionRequestVM.Area)} cannot be less than or equal to zero");
            }

            if (addRegionRequestVM.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Lat),
                    $"{nameof(addRegionRequestVM.Lat)} cannot be less than or equal to zero");
            }

            if (addRegionRequestVM.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Long),
                    $"{nameof(addRegionRequestVM.Long)} cannot be less than or equal to zero");
            }

            if (addRegionRequestVM.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequestVM.Population),
                    $"{nameof(addRegionRequestVM.Population)} cannot be less than zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateRegionAsync(UpdateRegionRequesrVM updateRegionRequesrVM)
        {
            if (updateRegionRequesrVM == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequesrVM), $"Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequesrVM.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequesrVM.Code),
                    $"{nameof(updateRegionRequesrVM.Code)} cannot be null or empty or white space");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequesrVM.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequesrVM.Name),
                    $"{nameof(updateRegionRequesrVM.Name)} cannot be null or empty or white space");
            }

            if (updateRegionRequesrVM.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequesrVM.Area),
                    $"{nameof(updateRegionRequesrVM.Area)} cannot be less than or equal to zero");
            }

            if (updateRegionRequesrVM.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequesrVM.Population),
                    $"{nameof(updateRegionRequesrVM.Population)} cannot be less than zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
