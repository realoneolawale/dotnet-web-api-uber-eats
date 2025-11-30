using Ubereats.DTO;
using Ubereats.Helpers;

namespace Ubereats.Repositories
{
    public interface IRestaurantRepository : IDisposable
    {
        Task<bool> IsRestaurantRegistered(int id);
        Task<MessageDto> RegisterRestaurant(RestaurantAddDto dto);
        Task<IEnumerable<RestaurantGetDto>> GetAllRestaurantsAsync();
    }
}