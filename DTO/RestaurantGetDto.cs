namespace Ubereats.DTO
{
    public class RestaurantGetDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string SecondaryName { get; set; }
        public string Description { get; set; }
        public string PlaceId { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string RestaurantBannerImage { get; set; }
    }
}