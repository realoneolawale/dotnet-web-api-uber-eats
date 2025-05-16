
using Microsoft.EntityFrameworkCore;
using Ubereats.Models;

namespace Ubereats.Data
{
    public class UberEatsContext : DbContext
    {
        public UberEatsContext(DbContextOptions<UberEatsContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantImage> RestaurantImages { get; set; }
        public DbSet<RestaurantFood> RestaurantFoods { get; set; }
    }
}