using AutoMapper;
using backend_challenge.Business.Dtos;
using backend_challenge.Business.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend_challenge.DataAccess.Helpers;
using backend_challenge.DataAccess.Configuration;

namespace backend_challenge.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly string _secretKey;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork uow, IConfiguration config, IMapper mapper)
        {
            _uow = uow;
            _secretKey = config.GetSection("Jwt").GetSection("SecretKey").ToString();
            _mapper = mapper;
        }

        public async Task<AuthDto> AuthenticateAsync(string email, string password)
        {
            var result = await _uow.Users.GetUserByEmailAsync(email);

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
            var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentUser.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, currentUser.Role.Name));

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

        public async Task<UserDto> GetProfile(string token)
        {
            if (token is null)
            {
                throw new AppException("Token vacío");
            }
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = token.Split(" ")[1];
            var data = jwtHandler.ReadJwtToken(jwt);
            var userEmail = data.Claims.First(claim => claim.Type == "nameid").Value;
            var userEntity = await _uow.Users.GetUserByEmailAsync(userEmail.ToLower().Trim());

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }
    }
}
