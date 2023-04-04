namespace backend_challenge.Services
{
    public interface IAuthService
    {
    public Task<bool> AuthenticateAsync(string username, string password);
        public Task<string> GenerateTokenAsync(string username, string password);
    }
}
