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

            var regionsDTOs= _mapper.Map<List<Region>>(regions);
            return Ok(regionsDTOs);
        }

    }
}
