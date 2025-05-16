using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubereats.Models
{
    public class RestaurantImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBannerImage { get; set; }
        public bool IsFoodImage { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}