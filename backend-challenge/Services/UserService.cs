using AutoMapper;
using backend_challenge.DataAccess;
using backend_challenge.Dto;
using backend_challenge.Helpers;
using backend_challenge.Models;
using backend_challenge.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
       
        public UserService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper; 
        }
        public async Task CreateAsync(CreateUserDto userDto)
        {
            userDto.Password = HashPass.HashPassword(userDto.Password);
          
            var entity = _mapper.Map<User>(userDto);

            entity.RoleId = 2;
            
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var userDto = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
                if( userDto is null)
                {
                    return false;
                }
                _db.Users.Remove(userDto);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
               throw new Exception("Ourrió un error");
            }
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            var result = await _db.Users.ToListAsync();
            var resultDto = _mapper.Map<List<UserDto>>(result);
            return resultDto;
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var result = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var resultDto = _mapper.Map<UserDto>(result);
            return resultDto;
        }

        public Task<UserDto> Update(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateUserExist(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
           return user == null ? false : true;
        }
    }
}
