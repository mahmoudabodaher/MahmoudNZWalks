using MahmoudNZWalks.API.Data;
using MahmoudNZWalks.API.Models.Domain;
using MahmoudNZWalks.API.Models.DTOs;
using MahmoudNZWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MahmoudNZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDiffecultiesController : Controller
    {
        private readonly IWalkDiffecultyRepository _iWalkDiffecultyRepository;
        public WalkDiffecultiesController(IWalkDiffecultyRepository iWalkDiffecultyRepository)
        {
            this._iWalkDiffecultyRepository = iWalkDiffecultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDiffeculties()
        {
            return Ok(await _iWalkDiffecultyRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{passedID:guid}")]
        [ActionName("GetWalkDiffecultyByID")]
        public async Task<IActionResult> GetWalkDiffecultyByID(Guid passedID)
        {
            var walkDiffeculty = await _iWalkDiffecultyRepository.GetByIDAsync(passedID);
            if (walkDiffeculty == null)
            {
                return NotFound();
            }
            var walkDiffecultyDTO = new WalkDiffecultyDTO()
            {
                ID = passedID,
                Code = walkDiffeculty.Code
            };
            return Ok(walkDiffecultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDiffeculty([FromBody]AddWalkDiffecultyRequest addWalkDiffecultyRequest)
        {
            if (!ValidateAddWalkDiffecultyRequest(addWalkDiffecultyRequest))
            {
                return BadRequest(ModelState);
            }


            var WalkDIffecultyModel = new WalkDiffeculty()
            {
                Code = addWalkDiffecultyRequest.Code
            };

            var walkDiffecultyDB = await _iWalkDiffecultyRepository.AddWalkDiffecultyAsync(WalkDIffecultyModel);
            var walkDiffecultyDTO = new WalkDiffecultyDTO()
            {
                ID = walkDiffecultyDB.ID,
                Code = walkDiffecultyDB.Code
            };
            return CreatedAtAction(nameof(GetWalkDiffecultyByID), new { passedID = walkDiffecultyDTO.ID }, walkDiffecultyDTO);
        }


        [HttpPut]
        [Route("{passedID:guid}")]
        public async Task<IActionResult> UpdateWalkDIffeculty([FromRoute]Guid passedID , [FromBody]UpdateWalkDiffecultyRequest updateWalkDiffecultyRequest)
        {

            if (!ValidateUpdateWalkDiffecultyRequest(updateWalkDiffecultyRequest))
            {
                return BadRequest(ModelState);
            }


            var walkDIffecultyDBModelForInsert = new WalkDiffeculty()
            {
                Code = updateWalkDiffecultyRequest.Code
            };

            var walkDiffecultyDB = await _iWalkDiffecultyRepository.UpdateWalkDiffecultyAsync(passedID, walkDIffecultyDBModelForInsert);
            if (walkDiffecultyDB == null)
            {
                return NotFound("No Walk Diffeculty was found");
            }
            var walkDiffecultyDTO = new WalkDiffecultyDTO()
            {
                ID = walkDiffecultyDB.ID,
                Code = walkDiffecultyDB.Code
            };
            return Ok(walkDiffecultyDTO);
        }
        
        [HttpDelete]
        [Route("{passedID:guid}")]
        public async Task<IActionResult> DeleteWalkDIffeculty(Guid passedID)
        {
            var walkDiffecultyDB = await _iWalkDiffecultyRepository.DeleteWalkDiffecultyAsync(passedID);
            if (walkDiffecultyDB == null)
            {
                return BadRequest("This diffeculty not existed or related with current added walks");
            }
            var walkDiffecultyDTO = new WalkDiffecultyDTO()
            {
                ID = walkDiffecultyDB.ID,
                Code = walkDiffecultyDB.Code
            };
            return Ok(walkDiffecultyDTO);
        }



        #region Private Methods
        private bool ValidateAddWalkDiffecultyRequest(AddWalkDiffecultyRequest addWalkDiffecultyRequest)
        {
            if (addWalkDiffecultyRequest == null)
            {
                ModelState.AddModelError(nameof(AddWalkDiffecultyRequest), $"Add Walk Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDiffecultyRequest.Code))
            {
                ModelState.AddModelError(nameof(AddWalkDiffecultyRequest.Code), $"{nameof(addWalkDiffecultyRequest.Code)} cannot be empty");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }

        private bool ValidateUpdateWalkDiffecultyRequest(UpdateWalkDiffecultyRequest updateWalkDiffecultyRequest)
        {
            if (updateWalkDiffecultyRequest == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkDiffecultyRequest), $"Add Walk Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDiffecultyRequest.Code))
            {
                ModelState.AddModelError(nameof(UpdateWalkDiffecultyRequest.Code), $"{nameof(updateWalkDiffecultyRequest.Code)} cannot be empty");

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
