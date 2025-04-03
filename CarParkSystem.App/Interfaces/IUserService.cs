using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Interfaces
{
    public interface IUserService
    {
        public Task CreateUserAsync(CreateUserDto createUserDto);
        public Task<UserDto> GetUserAsync(Guid id);
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();
        public Task<List<UserDto>> GetUsersByFilterAsync(int pageSize, int pageNumber, Expression<Func<User, bool>>? filter);
        public Task EditUserAsync(Guid id, CreateUserDto updatedUserDto);
        public Task RemoveUserAsync(Guid id);
        public Task<UserDto?> AuthenticateAsync(string username, string password);
    }
}
