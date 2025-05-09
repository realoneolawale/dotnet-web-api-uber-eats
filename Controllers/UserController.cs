
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ubereats.DTO;
using Ubereats.Helpers;
using Ubereats.Repositories;

namespace ubereats.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("is-authenticated")]
        public bool IsUserAuthenticated()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (userEmail is null)
                return false;
            return true;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok();
        }

        [HttpGet("verify-otp/{userId}/{otp}")]
        public async Task<IActionResult> VerifyLoggedInUserOTP(int userId, string otp)
        {
            if (otp != null && userId > 0)
                return Ok(await _userRepository.VerifyLoggedInUserOTP(userId, otp));
            return NotFound();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userRepository.Register(userDto));
            }
            return NotFound();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userRepository.Login(loginDto));
            }
            return NotFound();
        }


    }
}