using AFwalks.API.Models.Domain;
using AFwalks.API.Models.DTO;
using AFwalks.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFwalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            // Fetch data from database - domain walkdifficulty
            var walkDiff = await walkDifficultyRepository.GetAllAsync();

            // Convert domain walks to DTO Walks
            var walkDiffDTO = mapper.Map<List<WalkDifficultyVM>>(walkDiff);

            // Return response
            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            // Get walkdifficulty from databse
            var walkDiff = await walkDifficultyRepository.GetAsync(id);

            if (walkDiff == null)
            {
                return NotFound();
            }

            // Convert domain walkdifficulty to DTO walkdifficulty
            var walkDiffDTO = mapper.Map<WalkDifficultyVM>(walkDiff);

            //return response
            return Ok(walkDiffDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyRequestVM addWalkDifficultyRequestVM)
        {
            // Validate the Request
            var Validate = ValidateAddWalkDifficultyAsync(addWalkDifficultyRequestVM);

            if (!Validate)
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Domain Object
            var walkDiff = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequestVM.Code,
            };

            // Pass domain object to Repository
            walkDiff = await walkDifficultyRepository.AddAsync(walkDiff);

            // Convert the domain back to DTO

            //var walkDiffDTO = new WalkDifficultyVM()
            //{
            //    Id = walkDiff.Id,
            //    Code = walkDiff.Code,
            //};
            var walkDiffDTO = mapper.Map<WalkDifficultyVM>(walkDiff);  //using automapper

            // Send DTO response back to client
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDiffDTO.Id }, walkDiffDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, UpdateWalkDifficultyRequestVM updateWalkDifficultyRequestVM)
        {
            // Validate the Request
            var Validate = ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequestVM);

            if (!Validate)
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to domain model
            var walkDiff = new WalkDifficulty()
            {
                Code = updateWalkDifficultyRequestVM.Code,
            };

            // Update walk using repository
            walkDiff = await walkDifficultyRepository.UpdateAsync(id, walkDiff);

            // If null the Notfound
            if (walkDiff == null)
            {
                return NotFound();
            }

            // Convert domain back to DTO
            var walkDiffDTO = mapper.Map<WalkDifficultyVM>(walkDiff); //  using automapper

            //var walkDiffDTO = new WalkDifficultyVM()
            //{
            //    Id = walkDiff.Id,
            //    Code = walkDiff.Code,
            //};

            // Return Ok Response
            return Ok(walkDiffDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            // Call Repository to delete walk
            var walkDiff = await walkDifficultyRepository.DeleteAsync(id);

            // if null Notfound
            if (walkDiff == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var walkDiffDTO = new WalkDifficultyVM
            {
                Id = walkDiff.Id,
                Code = walkDiff.Code,
            };

            // return Ok response
            return Ok(walkDiffDTO);
        }


        #region Private Method
        private bool ValidateAddWalkDifficultyAsync(AddWalkDifficultyRequestVM addWalkDifficultyRequestVM)
        {
            if (addWalkDifficultyRequestVM == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequestVM), $"This is Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequestVM.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequestVM.Code),
                    $"{nameof(addWalkDifficultyRequestVM.Code)} is required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(UpdateWalkDifficultyRequestVM updateWalkDifficultyRequestVM)
        {
            if (updateWalkDifficultyRequestVM == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequestVM),
                    $"{nameof(updateWalkDifficultyRequestVM)} This is empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequestVM.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequestVM.Code),
                    $"{nameof(updateWalkDifficultyRequestVM.Code)} is required");
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
