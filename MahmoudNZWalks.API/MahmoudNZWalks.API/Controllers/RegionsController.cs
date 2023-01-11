using AutoMapper;
using MahmoudNZWalks.API.Models.Domain;
using MahmoudNZWalks.API.Models.DTOs;
using MahmoudNZWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MahmoudNZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {

        private readonly IRegionRepository _regionRepository;
        private readonly IMapper  _mapper;

        public RegionsController(IRegionRepository regionRepository , IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();
            //// Return the DTO instead
            //var regionsDTOs = new List<RegionDTO>();
            //regions.ToList().ForEach(region =>
            //{
            //    var singleRegionDTO = new RegionDTO()
            //    {
            //        ID = region.ID,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        population = region.population
            //    };
            //    regionsDTOs.Add(singleRegionDTO);
            //});

            var regionsDTOs= _mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTOs);
        }

        [HttpGet]
        [Route("{passedID:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid passedID)
        {
            var region = await _regionRepository.GetAsync(passedID);
            if (region == null)
            {
                return NotFound("Couldn't Get IT");
            }
            var regionsDTOs = _mapper.Map<RegionDTO>(region);
            return Ok(regionsDTOs);
        }


        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {

          ////  var valid = ;

          //  if (!ValidateAddRegionRequest(addRegionRequest))
          //  {
          //      return BadRequest(ModelState);
          //  }


            // convert request to domain model 
            var regionDBModel = new Region()
            {
                Area = addRegionRequest.Area,
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                population = addRegionRequest.population
            };
            // pass details to repository

            regionDBModel =  await _regionRepository.AddAsync(regionDBModel);
            // convert back to dto
            var regionDTO = new RegionDTO()
            {
                ID = regionDBModel.ID,
                Area = regionDBModel.Area,
                Code = regionDBModel.Code,
                Lat = regionDBModel.Lat,
                Long = regionDBModel.Long,
                Name = regionDBModel.Name,
                population = regionDBModel.population
            };

            return CreatedAtAction(nameof(GetRegionAsync),new { passedID = regionDTO.ID }, regionDTO);
        }

        [HttpDelete]
        [Route("{regionID:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid regionID)
        {
            // Get region from DB
            var regionToBeDeleted =await _regionRepository.DeleteAsync(regionID);

            // If not found
            if (regionToBeDeleted == null)
            {
                return NotFound();
            }

            // If found
            var regionDTO = new RegionDTO()
            {
                ID = regionToBeDeleted.ID,
                Area = regionToBeDeleted.Area,
                Code = regionToBeDeleted.Code,
                Lat = regionToBeDeleted.Lat,
                Long = regionToBeDeleted.Long,
                Name = regionToBeDeleted.Name,
                population = regionToBeDeleted.population
            };

            // Return
            return Ok(regionDTO);
         }


        [HttpPut]
        [Route("{passedID:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid passedID ,[FromBody] UpdateRegionRequest updateRegionRequest)
        {

            if (!ValidateUpdateRegionRequest(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }
            // Convert DTO to domain model
            var regionDBModel = new Region()
            {
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                population = updateRegionRequest.population
            };
            // Update using repository
          var region = await  _regionRepository.UpdateAsync(passedID, regionDBModel);
            // if Null not found
            if (region == null)
            {
                return NotFound();
            }
            // Convert domain back to DTO 
            var regionDTO = new RegionDTO()
            {
                ID = region.ID,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                population = region.population
            };
            // Return Ok response
            return Ok(regionDTO);
        }


        #region Private Methods
        private bool ValidateAddRegionRequest(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(AddRegionRequest), $"Add Region Data is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Name), $"{nameof(addRegionRequest.Name)} cannot be empty");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Lat), $"{nameof(addRegionRequest.Lat)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Long), $"{nameof(addRegionRequest.Long)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.population <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.population), $"{nameof(addRegionRequest.population)} cannot be less than or equal to zero");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegionRequest(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest), $"Add Region Data is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} cannot be empty");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Lat), $"{nameof(updateRegionRequest.Lat)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Long), $"{nameof(updateRegionRequest.Long)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.population <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.population), $"{nameof(updateRegionRequest.population)} cannot be less than or equal to zero");
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
