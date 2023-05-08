﻿using backend_challenge.Business.Dtos;

namespace backend_challenge.Business.Services
{
    public interface IUserService
    {
        public Task CreateAsync(CreateUserDto userDto);
        public Task<List<UserDto>> GetListAsync();
        public Task<UserDto> GetAsync(int id);
        public Task<bool> Update(UpdateUserDto userDto);
        public Task<bool> Delete(int id);
        public Task<bool> ValidateUserExist(string emial);
    }
}