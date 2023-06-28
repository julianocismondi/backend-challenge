using AutoMapper;
using backend_challenge.Business.Dtos;
using backend_challenge.Business.Helpers;
using backend_challenge.DataAccess.Configuration;
using backend_challenge.DataAccess.Helpers;
using backend_challenge.Domain.Entities;

namespace backend_challenge.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateUserDto userDto)
        {
            userDto.Password = HashPass.HashPassword(userDto.Password);
            var entity = _mapper.Map<User>(userDto);

            entity.RoleId = 2;

            await _uow.Users.Create(entity);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _uow.Users.Delete(id);
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);
            }
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            var users = await _uow.Users.GetAllUsersAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _uow.Users.GetUserByIdAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(UpdateUserDto updateUserDto)
        {
            User userEntity = await _uow.Users.GetById(updateUserDto.Id);
            userEntity.Name = updateUserDto.Name;
            userEntity.Email = updateUserDto.Email;
            userEntity.RoleId = updateUserDto.RoleId;

            await _uow.Users.Update(userEntity);
        }
    }
}
