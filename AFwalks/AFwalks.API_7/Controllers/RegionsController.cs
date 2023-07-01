using AFwalks.API_7.Data;
using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Models.DTO;
using AFwalks.API_7.Respositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AFwalks.API_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AFwalks7DbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(AFwalks7DbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionAsync()
        {
            //static data

            //var region = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Code = "NGA",
            //        Name= "Chuks Region",
            //        RegionImageUrl="https://wallpapercave.com/w/d8zqgVd"
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Code = "CND",
            //        Name = "Sagemode",
            //        RegionImageUrl = "https://wallpapercave.com/w/d8zqgVd"
            //    }
            //};

            // Get Data from Database - Domain models
          //  var regions = await dbContext.Regions.ToListAsync();
            var regions = await regionRepository.GetAllAsync();

            // Map domain models to DTOs
            var regionsDTO = new List<RegionDto>();
            //regions.ToList().ForEach(regionDOM =>
            //{
            //    var regionDTO = new RegionDto()
            //    {
            //        Id = regionDOM.Id,
            //        Name = regionDOM.Name,
            //        Code = regionDOM.Code,
            //        RegionImageUrl = regionDOM.RegionImageUrl,

            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            foreach (var regionDOM in regions)
            {
                regionsDTO.Add(new RegionDto()
                {
                    Id = regionDOM.Id,
                    Name = regionDOM.Name,
                    Code = regionDOM.Code,
                    RegionImageUrl = regionDOM.RegionImageUrl,
                });
            }

            //Return DTOs
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetByIdRegionAsync")]
        public async Task<IActionResult> GetByIdRegionAsync(Guid id)
        {
            var regions = await regionRepository.GetAsync(id);

            if (regions == null) 
            {
                return NotFound();
            }
            return Ok(regions);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domain Model
            var regionDomain = new Region()
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,

            };

            // Use Domain Model to create region
            //await dbContext.Regions.AddAsync(regionDomain);
            //await dbContext.SaveChangesAsync();
            regionDomain= await regionRepository.CreateAsync(regionDomain);

            // Map or convert back to DTO
            var regionDTO = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetByIdRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, UpdateRegionReguestDto updateRegionReguestDto)
        {
            // Check if region exist
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map DTO to domain model
            regionDomain.Name = updateRegionReguestDto.Name;
            regionDomain.Code = updateRegionReguestDto.Code;
            regionDomain.RegionImageUrl = updateRegionReguestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Convert Domain back to DTO
            var regionDTO = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Check if region exist
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Delete region
            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            // return deleted Region back
            // Map domain back to DTO

            var regionDTO = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDTO);
        }
    }
}
