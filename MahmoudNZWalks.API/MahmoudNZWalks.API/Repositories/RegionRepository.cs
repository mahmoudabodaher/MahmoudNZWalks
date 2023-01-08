using MahmoudNZWalks.API.Data;
using MahmoudNZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MahmoudNZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly MahmoudNZWalksDbContext _mahmoudNZWalksDbContext;

        public RegionRepository(MahmoudNZWalksDbContext mahmoudNZWalksDbContext)
        {
            this._mahmoudNZWalksDbContext = mahmoudNZWalksDbContext;
        }


        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await _mahmoudNZWalksDbContext.Regions.ToListAsync();
        }
    }
}
