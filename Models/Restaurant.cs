using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubereats.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SecondaryName { get; set; }
        public string Description { get; set; }
        public string PlaceId { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<RestaurantImage> RestaurantImages { get; set; }
        public List<RestaurantFood> RestaurantFoods { get; set; }
    }
}