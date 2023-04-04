using backend_challenge.Dto;

namespace backend_challenge.Services
{
    public interface IUserService
    {
        public Task CreateAsync(UserDto userDto);
        public Task<List<UserDto>> GetListAsync();

        public Task<UserDto> GetAsync(int id);

        public Task<UserDto> Update(UserDto userDto);
        public Task<bool> Delete(int id);

        public Task<bool> ValidateUserExist(string emial);


    }
}
