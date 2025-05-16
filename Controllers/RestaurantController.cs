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

    [HttpPost("register-restaurant")]
    public async Task<ActionResult<MessageDto>> RegisterRestaurant(RestaurantAddDto dto)
    {
        return Ok(await _restaurantRepository.RegisterRestaurant(dto));
    }
    

}