using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> AddUserAsync(UserDto userDto);
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IEnumerable<User>> GetUsers(int skip, int take);
    Task<User> UpdateUserAsync(UserDto userDto);
    Task<User> DeleteUserAsync(Guid id);
}