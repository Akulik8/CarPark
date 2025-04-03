using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Interfaces
{
    public interface IUserStorage
    {
        public Task AddUserAsync(User user);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<List<User>> GetAllUsersByFilterAsync(int pageSize, int pageNumber, Expression<Func<User, bool>>? filter);
        public Task UpdateUserAsync(Guid id, User newUser);
        public Task DeleteUserAsync(Guid id);
    }
}
