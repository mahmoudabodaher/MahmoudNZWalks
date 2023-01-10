using MahmoudNZWalks.API.Models.Domain;

namespace MahmoudNZWalks.API.Repositories
{
    public interface IWalkDiffecultyRepository
    {
        Task<IEnumerable<WalkDiffeculty>> GetAllAsync();
        Task<WalkDiffeculty> GetByIDAsync(Guid id);
    }
}
