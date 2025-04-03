using AutoMapper;
using CarParkSystem.App.DTOs;
using CarParkSystem.App.Helpers;
using CarParkSystem.App.Interfaces;
using CarParkSystem.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarParkSystem.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserStorage _userStorage;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserStorage userStorage, ILogger<UserService> logger, IMapper mapper)
        {
            _userStorage = userStorage;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            var users = await _userStorage.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            var hashedPassword = PasswordHasher.HashPassword(password);

            if (user.PasswordHash != hashedPassword)
                return null;

            user.LastLogin = DateTime.Now.ToUniversalTime(); // или ToLocalTime(), если хочешь хранить локально
            await _userStorage.UpdateUserAsync(user.UserID, user); // важно: сохранить обновление

            return _mapper.Map<UserDto>(user);
        }

        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            ValidateNewUser(createUserDto);

            var user = _mapper.Map<User>(createUserDto);
            user.UserID = Guid.NewGuid();
            user.Status = "On staff";
            user.PasswordHash = PasswordHasher.HashPassword(createUserDto.Password);
            user.LastLogin = DateTime.Now.ToUniversalTime();

            _logger.LogInformation("Creating user: {Username}", user.Username);
            await _userStorage.AddUserAsync(user);
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            _logger.LogInformation("Getting user by ID: {UserId}", id);
            var user = await _userStorage.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            _logger.LogInformation("Getting all users");
            var users = await _userStorage.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetUsersByFilterAsync(int pageSize, int pageNumber, Expression<Func<User, bool>>? filter)
        {
            _logger.LogInformation("Getting users by filter");
            var users = await _userStorage.GetAllUsersByFilterAsync(pageSize, pageNumber, filter);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task EditUserAsync(Guid id, CreateUserDto updatedDto)
        {
            ValidateNewUser(updatedDto);

            var updatedUser = new User
            {
                Username = updatedDto.Username,
                PasswordHash = PasswordHasher.HashPassword(updatedDto.Password),
                Name = updatedDto.Name,
                Surname = updatedDto.Surname,
                Role = updatedDto.Role,
                LastLogin = DateTime.Now.ToUniversalTime(),
                Status = updatedDto.Status
            };

            _logger.LogInformation("Editing user {UserId}", id);
            await _userStorage.UpdateUserAsync(id, updatedUser);
        }

        public async Task RemoveUserAsync(Guid id)
        {
            _logger.LogWarning("Deleting user {UserId}", id);
            await _userStorage.DeleteUserAsync(id);
        }

        private void ValidateNewUser(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ArgumentException("Имя пользователя обязательно.");

            if (!Regex.IsMatch(dto.Username, @"^[a-zA-Z0-9_]{3,20}$"))
                throw new ArgumentException("Имя пользователя должно состоять из букв и цифр и содержать от 3 до 20 символов.");

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ArgumentException("Пароль должен содержать не менее 6 символов.");

            if (string.IsNullOrWhiteSpace(dto.Role))
                throw new ArgumentException("Роль обязательна.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Имя обязательно.");

            if (string.IsNullOrWhiteSpace(dto.Surname))
                throw new ArgumentException("Фамилия обязательна.");

            if (string.IsNullOrWhiteSpace(dto.Status))
                throw new ArgumentException("Статус обязательен.");
        }
    }
}