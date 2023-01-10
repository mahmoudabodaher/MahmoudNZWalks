using AutoMapper;
using MahmoudNZWalks.API.Models.Domain;
using MahmoudNZWalks.API.Models.DTOs;
using MahmoudNZWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MahmoudNZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDiffecultyRepository _difficultyRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository , IWalkDiffecultyRepository difficultyRepository)
        {
            this._walkRepository = walkRepository;
            this._regionRepository = regionRepository;
            this._difficultyRepository = difficultyRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Ferch Data from DB (Domain)
            var walks = await _walkRepository.GetAllAsync();
            // COnvert Data to DTOs
            var WalksDTOs = _mapper.Map<List<WalkDTO>>(walks);
            // Return response
            return Ok(WalksDTOs);
        }

        [HttpGet]
        [Route("{passedID:guid}")]
        [ActionName("GetWalksByIdAsync")]
        public async Task<IActionResult> GetWalksByIdAsync(Guid passedID)
        {
            var Walk = await _walkRepository.GetWalkByIDAsync(passedID);
            if (Walk == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map<WalkDTO>(Walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            if (!await ValidateAddWalkRequest(addWalkRequest))
            {
              return  BadRequest(ModelState);
            }


            var walkModel = new Walk()
            {
                Lenght = addWalkRequest.Lenght,
                Name = addWalkRequest.Name,
                RegionID = addWalkRequest.RegionID,
                WalkDiffecultyID = addWalkRequest.WalkDiffecultyID,
            };
            var walkDB = await _walkRepository.AddWalkAsync(walkModel);
            var walkDTO = new WalkDTO()
            {
                ID = walkDB.ID,
                Lenght = walkDB.Lenght,
                Name = walkDB.Name,
                RegionID = walkDB.RegionID,
                WalkDiffecultyID = walkDB.WalkDiffecultyID
            };
            return CreatedAtAction(nameof(GetWalksByIdAsync), new { passedID = walkDTO.ID }, walkDTO);
        }

        [HttpPut]
        [Route("{passedID:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid passedID, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            if (! await ValidateUpdateWalkRequest(updateWalkRequest))
            {
                return BadRequest(ModelState);
            }
            var walk = new Walk()
            {
                Lenght = updateWalkRequest.Lenght,
                Name = updateWalkRequest.Name,
                RegionID = updateWalkRequest.RegionID,
                WalkDiffecultyID = updateWalkRequest.WalkDiffecultyID,
            };
            var walkDBModel = await _walkRepository.UpdateWalkAsync(passedID, walk);
            if (walkDBModel == null)
            {
                return NotFound("Walk with this ID not found");
            }
            var walkDTO = new WalkDTO()
            {
                ID = walkDBModel.ID,
                Lenght = walkDBModel.Lenght,
                Name = walkDBModel.Name,
                RegionID = walkDBModel.RegionID,
                WalkDiffecultyID = walkDBModel.WalkDiffecultyID,
            };
            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{passedID:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid passedID)
        {
            var walkFromDB = await _walkRepository.DeleteWalkAsync(passedID);
            if (walkFromDB == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDTO>(walkFromDB);
            return Ok(walkDTO);
        }



        #region Private Methods

        private async Task<bool> ValidateAddWalkRequest(AddWalkRequest addWalkRequest)
        {
            //Normal Validators

            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(AddWalkRequest), $"Add Walk Data is required");
                return false;
            }
            if (addWalkRequest.Lenght <= 0)
            {
                ModelState.AddModelError(nameof(AddWalkRequest.Lenght), $"{nameof(addWalkRequest.Lenght)} cannot be empty");

            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(AddWalkRequest.Name), $"{nameof(addWalkRequest.Name)} cannot be empty");

            }


            var region = await _regionRepository.GetAsync(addWalkRequest.RegionID);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionID), $"{nameof(addWalkRequest.RegionID)} Not found in Regions");

            }


            var walkDiffeculty = await _difficultyRepository.GetByIDAsync(addWalkRequest.WalkDiffecultyID);

            if (walkDiffeculty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDiffecultyID), $"{nameof(addWalkRequest.WalkDiffecultyID)} Not found in Walk Diffeculties");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkRequest(UpdateWalkRequest updateWalkRequest)
        {
            //Normal Validators

            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest), $"Add Walk Data is required");
                return false;
            }
            if (updateWalkRequest.Lenght <= 0)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.Lenght), $"{nameof(updateWalkRequest.Lenght)} cannot be empty");

            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} cannot be empty");

            }


            var region = await _regionRepository.GetAsync(updateWalkRequest.RegionID);
            if (region == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.RegionID), $"{nameof(updateWalkRequest.RegionID)} Not found in Regions");

            }


            var walkDiffeculty = await _difficultyRepository.GetByIDAsync(updateWalkRequest.WalkDiffecultyID);

            if (walkDiffeculty == null)
            {
                ModelState.AddModelError(nameof(UpdateWalkRequest.WalkDiffecultyID), $"{nameof(updateWalkRequest.WalkDiffecultyID)} Not found in Walk Diffeculties");

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
