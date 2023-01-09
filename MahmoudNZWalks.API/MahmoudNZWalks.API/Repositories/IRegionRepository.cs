using MahmoudNZWalks.API.Models.Domain;

namespace MahmoudNZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid ID);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid ID);

        Task<Region> UpdateAsync(Guid ID, Region region);
    }
}
