using MahmoudNZWalks.API.Data;
using MahmoudNZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MahmoudNZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly MahmoudNZWalksDbContext _mahmoudNZWalksDbContext;

        public WalkRepository(MahmoudNZWalksDbContext mahmoudNZWalksDbContext)
        {
            this._mahmoudNZWalksDbContext = mahmoudNZWalksDbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.ID = new Guid();
            await _mahmoudNZWalksDbContext.Walks.AddAsync(walk);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid walkID)
        {
           var existingWalk =  await _mahmoudNZWalksDbContext.Walks.FindAsync(walkID);
            if (existingWalk == null)
            {
                return null;
            }
            _mahmoudNZWalksDbContext.Walks.Remove(existingWalk);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _mahmoudNZWalksDbContext.Walks
                 .Include(x => x.Region)
                 .Include(x => x.WalkDiffeculty)
                 .ToListAsync();
        }

        public async Task<Walk> GetWalkByIDAsync(Guid id)
        {
            return await _mahmoudNZWalksDbContext.Walks
     .Include(x => x.Region)
     .Include(x => x.WalkDiffeculty)
     .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid walkID, Walk walk)
        {
            var existingwalk = await _mahmoudNZWalksDbContext.Walks.Include(w =>w.Region).Include(w =>w.WalkDiffeculty).FirstOrDefaultAsync(w => w.ID == walkID);
            if (existingwalk != null)
            {
                existingwalk.Name = walk.Name;
                existingwalk.WalkDiffecultyID = walk.WalkDiffecultyID;
                existingwalk.Lenght = walk.Lenght;
                existingwalk.RegionID = walk.RegionID;
                 _mahmoudNZWalksDbContext.Walks.Update(existingwalk);
                await _mahmoudNZWalksDbContext.SaveChangesAsync();
                return existingwalk;
            }
            return null;
        }
    }
}
