using Ubereats.DTO;
using Ubereats.Helpers;

namespace Ubereats.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<string> Register(UserDto userDto);
        string HashPasswordToSHA256(string s);
        Task<bool> VerifyLoggedInUserOTP(int userId, string otp);
    }
}