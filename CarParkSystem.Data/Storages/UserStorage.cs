using CarParkSystem.App.Interfaces;
using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarParkSystem.Data.Storages
{
    public class UserStorage : IUserStorage
    {
        private readonly CarParkSystemDbContext _carParkSystemDbContext;

        public UserStorage(CarParkSystemDbContext carParkSystemDbContext)
        {
            _carParkSystemDbContext = carParkSystemDbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _carParkSystemDbContext.Users.AddAsync(user);
            await _carParkSystemDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _carParkSystemDbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _carParkSystemDbContext.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsersByFilterAsync(int pageSize, int pageNumber, Expression<Func<User, bool>>? filter)
        {
            var query = _carParkSystemDbContext.Users.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query
                .OrderBy(x => x.Username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task UpdateUserAsync(Guid id, User newUser)
        {
            var user = await _carParkSystemDbContext.Users
                       .FirstOrDefaultAsync(a => a.UserID == id);

            if (user != null)
            {
                user.Surname = newUser.Surname;
                user.Name = newUser.Name;
                user.Status = newUser.Status;
                user.Username = newUser.Username;
                user.PasswordHash = newUser.PasswordHash;
                user.Role = newUser.Role;
                user.LastLogin = newUser.LastLogin;

                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _carParkSystemDbContext.Users
                      .FirstOrDefaultAsync(a => a.UserID == id);
            if (user != null)
            {
                _carParkSystemDbContext.Users.Remove(user);
                await _carParkSystemDbContext.SaveChangesAsync();
            }
        }
    }
}