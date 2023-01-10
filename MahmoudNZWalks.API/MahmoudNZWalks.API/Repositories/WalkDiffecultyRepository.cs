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
        public async Task<IEnumerable<WalkDiffeculty>> GetAllAsync()
        {
            return await _mahmoudNZWalksDbContext.WalkDiffeculty.ToListAsync();
        }

        public async Task<WalkDiffeculty> GetByIDAsync(Guid ID)
        {
            return await _mahmoudNZWalksDbContext.WalkDiffeculty.FirstOrDefaultAsync(w => w.ID == ID);
        }
    }
}
