using backend_challenge.Business.Dtos;

namespace backend_challenge.Business.Services
{
    public interface IAuthService
    {
        public Task<AuthDto> AuthenticateAsync(string username, string password);
        public Task<string> GenerateTokenAsync(string username, string password);
        public Task<UserDto> GetProfile(string token);
    }
}
