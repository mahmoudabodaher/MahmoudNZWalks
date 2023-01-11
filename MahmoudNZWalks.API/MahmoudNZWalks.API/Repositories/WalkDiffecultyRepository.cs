using MahmoudNZWalks.API.Data;
using MahmoudNZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MahmoudNZWalks.API.Repositories
{
    public class WalkDiffecultyRepository : IWalkDiffecultyRepository
    {
        private readonly MahmoudNZWalksDbContext _mahmoudNZWalksDbContext;

        public WalkDiffecultyRepository(MahmoudNZWalksDbContext mahmoudNZWalksDbContext)
        {
            this._mahmoudNZWalksDbContext = mahmoudNZWalksDbContext;
        }

        public async Task<WalkDiffeculty> AddWalkDiffecultyAsync(WalkDiffeculty walkDiffeculty)
        {
            walkDiffeculty.ID = Guid.NewGuid();
            await _mahmoudNZWalksDbContext.WalkDiffeculty.AddAsync(walkDiffeculty);
            await _mahmoudNZWalksDbContext.SaveChangesAsync();
            return walkDiffeculty;
        }

        public async Task<WalkDiffeculty> DeleteWalkDiffecultyAsync(Guid id)
        {
            var walkDiffecultyFromDB = await _mahmoudNZWalksDbContext.WalkDiffeculty.FirstOrDefaultAsync(x => x.ID == id);
            if (walkDiffecultyFromDB != null)
            {
                var walkFromDB = await _mahmoudNZWalksDbContext.Walks.FirstOrDefaultAsync(w => w.WalkDiffecultyID == id);
                if (walkFromDB != null)
                {
                    return null;
                }
                _mahmoudNZWalksDbContext.WalkDiffeculty.Remove(walkDiffecultyFromDB);
                await _mahmoudNZWalksDbContext.SaveChangesAsync();
                return walkDiffecultyFromDB;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDiffeculty>> GetAllAsync()
        {
            return await _mahmoudNZWalksDbContext.WalkDiffeculty.ToListAsync();
        }

        public async Task<WalkDiffeculty> GetByIDAsync(Guid ID)
        {
            return await _mahmoudNZWalksDbContext.WalkDiffeculty.FirstOrDefaultAsync(w => w.ID == ID);
        }

        public async Task<WalkDiffeculty> UpdateWalkDiffecultyAsync(Guid id, WalkDiffeculty walkDiffeculty)
        {
            var walkDiffecultyDB = await _mahmoudNZWalksDbContext.WalkDiffeculty.FirstOrDefaultAsync(w => w.ID == id);
            if (walkDiffecultyDB != null)
            {
                walkDiffecultyDB.Code = walkDiffeculty.Code;
                _mahmoudNZWalksDbContext.WalkDiffeculty.Update(walkDiffecultyDB);
                await _mahmoudNZWalksDbContext.SaveChangesAsync();
                return walkDiffecultyDB;
            }
            return null;
        }
    }
}
