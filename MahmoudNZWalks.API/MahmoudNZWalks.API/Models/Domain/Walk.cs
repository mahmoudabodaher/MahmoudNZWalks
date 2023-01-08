namespace MahmoudNZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double   Lenght { get; set; }
        public Guid  RegionID { get; set; }
        public Guid WalkDiffecultyID { get; set; }
        //  Navigation Properties
        public Region Region { get; set; }
        public WalkDiffeculty WalkDiffeculty { get; set; }
    }
}
