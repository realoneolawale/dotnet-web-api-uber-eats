using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Ubereats.Data;
using Ubereats.DTO;
using Ubereats.Helpers;
using Ubereats.Models;

namespace Ubereats.Repositories
{

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly UberEatsContext _context;

        public RestaurantRepository(UberEatsContext context)
        {
            _context = context;
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
                Restaurant restaurant = new Restaurant
                {
                    Address = dto.Address,
                    Name = dto.Name,
                    SecondaryName = dto.SecondaryName,
                    Description = dto.Description,
                    PlaceId = dto.PlaceId,
                    Longitude = dto.Longitude,
                    Latitude = dto.Latitude
                };
                await _context.Restaurants.AddAsync(restaurant);
                return new MessageDto(await _context.SaveChangesAsync(), "Restaurant added successful", string.Empty);
            }
            throw new UberEatsException("Restaurant already exists", HttpStatusCode.Conflict);
        }

        private async Task<bool> RestaurantExists(string name) {
            return await _context.Restaurants.AnyAsync(x => x.Name == name);
        }
    }
}