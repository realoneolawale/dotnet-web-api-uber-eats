using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubereats.Models
{
    public class RestaurantFood
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<RestaurantImage> RestaurantImages { get; set; }
    }
}