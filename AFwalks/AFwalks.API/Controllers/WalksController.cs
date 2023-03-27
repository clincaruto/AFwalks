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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository  walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            // Validate the Request
            var Validate =await ValidateAddWalkAsync(addWalkRequestVM);
            if (!Validate)
            {
                return BadRequest(ModelState);
            }

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
            // Validate the Request
            var Validate = await ValidateUpdateWalkAsync(updateWalkRequestVM);
            if (!Validate)
            {
                return BadRequest(ModelState);
            }

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

        #region Private Method

        private async Task<bool> ValidateAddWalkAsync(AddWalkRequestVM addWalkRequestVM)
        {
            if (addWalkRequestVM == null)
            {
                ModelState.AddModelError(nameof(addWalkRequestVM),
                    $"{nameof(addWalkRequestVM)} cannot be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkRequestVM.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequestVM.Name),
                    $"{nameof(addWalkRequestVM.Name)} is required");
            }

            if (addWalkRequestVM.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequestVM.Length),
                    $"{nameof(addWalkRequestVM.Length)} should be greater than zero");
            }

            var region = await regionRepository.GetAsync(addWalkRequestVM.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequestVM.RegionId),
                    $"{nameof(addWalkRequestVM.RegionId)} is Invalid ");
            }

            var walkDiff = await walkDifficultyRepository.GetAsync(addWalkRequestVM.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(addWalkRequestVM.WalkDifficultyId),
                    $"{nameof(addWalkRequestVM.WalkDifficultyId)} is Invalid ");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;



        }

        private async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequestVM updateWalkRequestVM)
        {
            if (updateWalkRequestVM == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequestVM),
                    $"{nameof(updateWalkRequestVM)} cannot be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkRequestVM.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequestVM.Name),
                    $"{nameof(updateWalkRequestVM.Name)} is required");
            }

            if (updateWalkRequestVM.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequestVM.Length),
                    $"{nameof(updateWalkRequestVM.Length)} should be greater than zero");
            }

            var region = await regionRepository.GetAsync(updateWalkRequestVM.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequestVM.RegionId),
                    $"{nameof(updateWalkRequestVM.RegionId)} is Invalid ");
            }

            var walkDiff = await walkDifficultyRepository.GetAsync(updateWalkRequestVM.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequestVM.WalkDifficultyId),
                    $"{nameof(updateWalkRequestVM.WalkDifficultyId)} is Invalid ");
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
