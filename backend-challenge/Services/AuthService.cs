using backend_challenge.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_challenge.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly string _secretKey;
        public AuthService(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _secretKey = config.GetSection("Jwt").GetSection("SecretKey").ToString();
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return result == null ? false : true;
        }

        public async Task<string> GenerateTokenAsync(string email, string password)
        {
            if (await AuthenticateAsync(email, password))
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, email));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string generatedToken = tokenHandler.WriteToken(tokenConfig);
                return generatedToken;

            }
            throw new Exception("Usuario no encontrado");

        }
    }

}
