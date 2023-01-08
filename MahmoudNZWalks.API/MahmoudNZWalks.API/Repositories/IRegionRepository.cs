using MahmoudNZWalks.API.Models.Domain;

namespace MahmoudNZWalks.API.Repositories
{
    public interface IRegionRepository
    {
      Task<IEnumerable<Region>>  GetAllAsync();
    }
}
