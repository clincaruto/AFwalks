using AFwalks.API_7.Models.Domain;
using AFwalks.API_7.Models.DTO;
using AFwalks.API_7.Respositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Get data from database - domain model
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Create an exception
           // throw new Exception("This is a new exception");

            // Map domain model to Dtos
            var walksDTO = mapper.Map<List<WalkDto>>(walks);

            // Return DTOs
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map/ Convert DTO to Domain Model
                var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

                // Use domain model to creae region
                walkDomain = await walkRepository.CreateAsync(walkDomain);

                // Map Domain to DTO
                var walkDTO = mapper.Map<WalkDto>(walkDomain);

                return Ok(walkDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
           
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdWalkAsync(Guid id)
        {
            var walks = await walkRepository.GetAsync(id);

            if (walks == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var walksDTO = mapper.Map<WalkDto>(walks);

            return Ok(walksDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to domain model
                var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);

                // check if walks exist
                walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
                if (walkDomain == null)
                {
                    return NotFound();
                }

                // Map domain back to DTO
                var walkDTO = mapper.Map<WalkDto>(walkDomain);
                return Ok(walkDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
           
            // check if walks exist
            var walkDomain = await walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map domain back to DTO
            var walkDTO = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
