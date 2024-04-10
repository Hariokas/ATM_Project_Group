using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces;

public interface IUserService
{
    Task<User> AddUserAsync(UserDto user);
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> UpdateUserAsync(UserDto user);
    Task<User> DeleteUserAsync(Guid id);
}