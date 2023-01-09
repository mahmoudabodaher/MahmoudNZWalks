namespace MahmoudNZWalks.API.Models.DTOs
{
    public class WalkDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Lenght { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDiffecultyID { get; set; }
        //  Navigation Properties
        public RegionDTO Region { get; set; }
        public WalkDiffecultyDTO WalkDiffeculty { get; set; }
    }
}
