namespace MahmoudNZWalks.API.Models.DTOs
{
    public class UpdateWalkRequest
    {
        public string Name { get; set; }
        public double Lenght { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDiffecultyID { get; set; }
    }
}
