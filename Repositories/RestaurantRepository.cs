using System.Net;
using Microsoft.EntityFrameworkCore;
using Ubereats.Data;
using Ubereats.DTO;
using Ubereats.Helpers;
using Ubereats.Models;

namespace Ubereats.Repositories
{

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly UberEatsContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public RestaurantRepository(UberEatsContext context, IWebHostEnvironment env, IConfiguration config)
        {
            _context = context;
            _env = env;
            _config = config;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> IsRestaurantRegistered(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return true;
            }
            return false;
        }

        public async Task<MessageDto> RegisterRestaurant(RestaurantAddDto dto)
        {
            if (!await RestaurantExists(dto.Name))
            {
                // check for restaurant logo 
                if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
                    throw new UberEatsException("No restaurant logo added", HttpStatusCode.BadRequest);
                // upload the restaurant logo 
                var uploadPath = Path.Combine(_env.WebRootPath, "images/logos");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                var fileExtension = Path.GetExtension(dto.ImageUrl.FileName);
                var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadPath, newFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageUrl.CopyToAsync(stream);
                }
                Restaurant restaurant = new Restaurant
                {
                    Address = dto.Address,
                    Name = dto.Name,
                    UserId = dto.UserId,
                    SecondaryName = dto.SecondaryName,
                    Description = dto.Description,
                    ImageUrl = $"{_config["App:Domain"]}images/logos/{newFileName}",
                    PlaceId = dto.PlaceId,
                    Longitude = dto.Longitude,
                    Latitude = dto.Latitude
                };
                await _context.Restaurants.AddAsync(restaurant);
                return new MessageDto(await _context.SaveChangesAsync(), "Restaurant added successful", string.Empty);
            }
            throw new UberEatsException("Restaurant already exists", HttpStatusCode.Conflict);
        }

        public async Task<IEnumerable<RestaurantGetDto>> GetAllRestaurantsAsync()
        {
            var restaurants = await _context.Restaurants.Include(x => x.RestaurantImages).ToListAsync();
            if (restaurants != null)
                return restaurants.Select(x => new RestaurantGetDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    Name = x.Name,
                    Description = x.Description,
                    SecondaryName = x.SecondaryName,
                    PlaceId = x.PlaceId,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    RestaurantBannerImage = x.RestaurantImages.Count > 0 ? x.RestaurantImages.FirstOrDefault().ImageUrl : ""
                }).ToList();
            throw new UberEatsException("Restaurants not found", HttpStatusCode.NotFound);
        }

        private async Task<bool> RestaurantExists(string name)
        {
            return await _context.Restaurants.AnyAsync(x => x.Name == name);
        }
    }
}