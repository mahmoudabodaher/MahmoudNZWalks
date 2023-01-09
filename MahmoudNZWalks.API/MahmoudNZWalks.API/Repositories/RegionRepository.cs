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

        public async Task<Region> GetAsync(Guid ID)
        {
            return await _mahmoudNZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.ID == ID);
        }

        public async Task<Region> AddAsync(Region singleRegion)
        {
            singleRegion.ID = new Guid();
            await _mahmoudNZWalksDbContext.Regions.AddAsync(singleRegion);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return (singleRegion);
        }

        public async Task<Region> DeleteAsync(Guid regionID)
        {
            var region = await _mahmoudNZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.ID == regionID);
            if (region == null)
            {
                return null;
            }
            _mahmoudNZWalksDbContext.Regions.Remove(region);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid regionID , Region regionToBeUpdated)
        {
            var region = await _mahmoudNZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.ID == regionID);
            if (region == null)
            {
                return null;
            }
            region.population = regionToBeUpdated.population;
            region.Name = regionToBeUpdated.Name;
            region.Code = regionToBeUpdated.Code;
            region.Area = regionToBeUpdated.Area;
            region.Lat = regionToBeUpdated.Lat;
            region.Long = regionToBeUpdated.Long;

            _mahmoudNZWalksDbContext.Regions.Update(region);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return region;
        }
    }
}
