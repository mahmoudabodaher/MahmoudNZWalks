using MahmoudNZWalks.API.Models.Domain;

namespace MahmoudNZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkByIDAsync(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid walkID , Walk walk);

        Task<Walk> DeleteWalkAsync(Guid walkID);
    }
}
