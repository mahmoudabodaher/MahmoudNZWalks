using MahmoudNZWalks.API.Data;
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
    }
}
