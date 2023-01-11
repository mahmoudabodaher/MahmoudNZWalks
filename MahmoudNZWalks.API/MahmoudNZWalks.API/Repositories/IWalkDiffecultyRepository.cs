using MahmoudNZWalks.API.Models.Domain;

namespace MahmoudNZWalks.API.Repositories
{
    public interface IWalkDiffecultyRepository
    {
        Task<IEnumerable<WalkDiffeculty>> GetAllAsync();
        Task<WalkDiffeculty> GetByIDAsync(Guid id);
        Task<WalkDiffeculty> AddWalkDiffecultyAsync(WalkDiffeculty walkDiffeculty);
        Task<WalkDiffeculty> UpdateWalkDiffecultyAsync(Guid id, WalkDiffeculty walkDiffeculty);
        Task<WalkDiffeculty> DeleteWalkDiffecultyAsync(Guid id);
    }
}
