using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AFwalks.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository  walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            // Fetch data from database - domain walks
            var walks = await walkRepository.GetAllAsync();

            // Convert domain walks to DTO Walks
            var walksDTO = mapper.Map<List<WalkVM>>(walks);

            // Return response
            return Ok(walksDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get walk Domain pbject from database
            var walk = await walkRepository.GetAsync(id);


            if(walk == null)
            {
                return NotFound();
            }

            // convert domain walks to DTO walks
            var walksDTO = mapper.Map<WalkVM>(walk);

            // return response
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequestVM addWalkRequestVM)
        {
            // Convert DTO to Domain Object
            var walk = new Walk()
            {
                Name = addWalkRequestVM.Name,
                Length = addWalkRequestVM.Length,
                RegionId = addWalkRequestVM.RegionId,
                WalkDifficultyId = addWalkRequestVM.WalkDifficultyId,
            };

            // Pass domain object to  Repository
            walk = await walkRepository.AddAsync(walk);

            // Convert the domain back to DTO
            var walkDTO = new WalkVM
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // Send DTO response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id}, walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestVM updateWalkRequestVM)
        {
            // Convert DTO to domain model
            var walk = new Walk()
            {
                Length = updateWalkRequestVM.Length,
                Name = updateWalkRequestVM.Name,
                RegionId = updateWalkRequestVM.RegionId,
                WalkDifficultyId = updateWalkRequestVM.WalkDifficultyId,
            };

            // Update walk using repository
            walk = await walkRepository.UpdateAsync(id, walk);

            // if null the NotFound
            if (walk == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var walkDTO = new WalkVM
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // Return ok response
            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Call Repository to delete walk
            var walk = await walkRepository.DeleteAsync(id);

            // if null Notfound
            if (walk == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var walkDTO = new WalkVM
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            // return Ok response
            return Ok(walkDTO);
        }

    }
}
