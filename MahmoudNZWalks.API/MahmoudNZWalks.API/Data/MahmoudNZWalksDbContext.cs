using MahmoudNZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MahmoudNZWalks.API.Data
{
    public class MahmoudNZWalksDbContext : DbContext
    {
        public MahmoudNZWalksDbContext(DbContextOptions<MahmoudNZWalksDbContext> options): base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet <WalkDiffeculty> WalkDiffeculty { get; set; }

    }
}
