using AutoMapper;
using MahmoudNZWalks.API.Models.Domain;
using MahmoudNZWalks.API.Models.DTOs;

namespace MahmoudNZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Walk, WalkDTO>().ReverseMap();

            CreateMap<WalkDiffeculty, WalkDiffecultyDTO>().ReverseMap();

        }
    }
}
