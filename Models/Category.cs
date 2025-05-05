using System.ComponentModel.DataAnnotations;

namespace Ubereats.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}