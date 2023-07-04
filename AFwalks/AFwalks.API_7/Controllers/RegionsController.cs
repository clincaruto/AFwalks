using AFwalks.API_7.Data;
using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Models.DTO;
using AFwalks.API_7.Respositories.IRepository;
using AFwalks.API_7.Validators.CustomActionFilters;
using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(AFwalks7DbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
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

            //var regionsDTO = new List<RegionDto>();
            //foreach (var regionDOM in regions)
            //{
            //    regionsDTO.Add(new RegionDto()
            //    {
            //        Id = regionDOM.Id,
            //        Name = regionDOM.Name,
            //        Code = regionDOM.Code,
            //        RegionImageUrl = regionDOM.RegionImageUrl,
            //    });
            //}

            var regionsDTO = mapper.Map<List<RegionDto>>(regions);

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

            // Map Region Domain Model to Region DTO
            var regionsDTO = mapper.Map<RegionDto>(regions);

            return Ok(regionsDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domain Model
            //var regionDomain = new Region()
            //{
            //    Name = addRegionRequestDto.Name,
            //    Code = addRegionRequestDto.Code,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,

            //};
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create region
            //await dbContext.Regions.AddAsync(regionDomain);
            //await dbContext.SaveChangesAsync();
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            // Map or convert back to DTO
            //var regionDTO = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetByIdRegionAsync), new { id = regionDTO.Id }, regionDTO);

        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, UpdateRegionReguestDto updateRegionReguestDto)
        {
            // Map DTO to domain model

            //var regionDomain = new Region
            //{
            //    Name = updateRegionReguestDto.Name,
            //    Code = updateRegionReguestDto.Code,
            //    RegionImageUrl = updateRegionReguestDto.RegionImageUrl
            //};

            var regionDomain = mapper.Map<Region>(updateRegionReguestDto);

            // Check if region exist
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO

            //var regionDTO = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Check if region exist
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

           
            // return deleted Region back
            // Map domain back to DTO

            var regionDTO = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDTO);
        }
    }
}
