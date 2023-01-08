using AutoMapper;
using MahmoudNZWalks.API.Models.Domain;
using MahmoudNZWalks.API.Models.DTOs;

namespace MahmoudNZWalks.API.Profiles
{
    public class RegionsProfiles : Profile
    {
        public RegionsProfiles()
        {
            // If needed to map with spectial cases
            //CreateMap<Region, RegionDTO>()
            //    .ForMember(dest => dest.ID, options => options.MapFrom(src => src.ID));


            // To start mapping for the both ways
            CreateMap<Region, RegionDTO>().ReverseMap();
        }
    }
}
