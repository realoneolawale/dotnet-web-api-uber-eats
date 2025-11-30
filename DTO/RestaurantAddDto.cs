using System.ComponentModel.DataAnnotations;

namespace Ubereats.DTO
{
    public class RestaurantAddDto
    {
        [Required(ErrorMessage = "Restaurant address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Restaurant name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Restaurant secondary name is required")]
        public string SecondaryName { get; set; }
        [Required(ErrorMessage = "Restaurant description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Restaurant logo is required")]
        public IFormFile ImageUrl { get; set; }
        [Required(ErrorMessage = "User id is required")]
        public int UserId { get; set; }
        public string PlaceId { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
    }
}