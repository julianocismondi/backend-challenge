using AutoMapper;
using backend_challenge.Business.Dtos;
using backend_challenge.Business.Helpers;
using backend_challenge.DataAccess.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using backend_challenge.DataAccess.Helpers;

namespace backend_challenge.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly string _secretKey;
        private readonly IMapper _mapper;
        public AuthService(ApplicationDbContext db, IConfiguration config, IMapper mapper)
        {
            _db = db;
            _secretKey = config.GetSection("Jwt").GetSection("SecretKey").ToString();
            _mapper = mapper;
        }

        public async Task<AuthDto> AuthenticateAsync(string email, string password)
        {
            var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (result is null)
            {
                throw new KeyNotFoundException("No se encontró el usuario");
            };
            if (!HashPass.VerifyPassword(password, result.Password))
            {
                throw new AppException("Contraseña incorrecta");
            }
            var authDto = _mapper.Map<AuthDto>(result);
            return authDto;
        }

        public async Task<string> GenerateTokenAsync(string email, string password)
        {
            var currentUser = await AuthenticateAsync(email, password);
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == currentUser.RoleId);
            var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentUser.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, role.Name));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(tokenConfig);
            return tokenString;
        }

        public async Task<object> GetProfile(string token)
        {
            if (token is null)
            {
                throw new AppException("Token vacío");
            }
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = token.Split(" ")[1];
            var data = jwtHandler.ReadJwtToken(jwt);
            var userEmail = data.Claims.First(claim => claim.Type == "nameid").Value;
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            var obj = new
            {
                user.Id,
                user.Name,
                user.Email,
                Role = role.Name
            };

            return obj;
        }
    }
}
