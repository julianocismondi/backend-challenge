using backend_challenge.Models.Dto;

namespace backend_challenge.Services
{
    public interface IAuthService
    {
        public Task<AuthDto> AuthenticateAsync(string username, string password);
        public Task<string> GenerateTokenAsync(string username, string password);
        public Task<object> GetProfile(string token);
    }
}
