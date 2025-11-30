using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ubereats.DTO;
using Ubereats.Helpers;
using Ubereats.Repositories;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantController(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }


    [HttpGet("is-restaurant-registered")]
    public async Task<ActionResult<bool>> IsRestaurantRegistered(int id)
    {
        return Ok(await _restaurantRepository.IsRestaurantRegistered(id));
    }

    [Authorize]
    [HttpPost("register-restaurant")]
    public async Task<ActionResult<MessageDto>> RegisterRestaurant(RestaurantAddDto dto)
    {
        return Ok(await _restaurantRepository.RegisterRestaurant(dto));
    }

    [HttpGet("get-all-restaurants")]
    public async Task<ActionResult<RestaurantGetDto>> GetAllRestaurants()
    {
        return Ok(await _restaurantRepository.GetAllRestaurantsAsync());
    }

}