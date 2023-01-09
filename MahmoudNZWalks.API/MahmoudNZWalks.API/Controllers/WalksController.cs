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

        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this._walkRepository = walkRepository;
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
          var walkFromDB = await  _walkRepository.DeleteWalkAsync(passedID);
            if (walkFromDB == null)
            {
                return NotFound();
            }
           var walkDTO =  _mapper.Map<WalkDTO>(walkFromDB);
            return Ok(walkDTO);
        }


    }
}
