using backend_challenge.Business.Dtos;

namespace backend_challenge.Business.Services
{
    public interface IUserService
    {
        public Task CreateAsync(CreateUserDto userDto);
        public Task<List<UserDto>> GetListAsync();
        public Task<UserDto> GetAsync(int id);
        public Task UpdateAsync(UpdateUserDto userDto);
        public Task DeleteAsync(int id);
    }
}
