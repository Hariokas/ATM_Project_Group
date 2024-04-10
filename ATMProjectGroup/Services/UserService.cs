using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<User> AddUserAsync(UserDto user)
    {
        return userRepository.AddUserAsync(user);
    }

    public Task<User> GetUserByIdAsync(Guid id)
    {
        return userRepository.GetUserByIdAsync(id);
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        return userRepository.GetUserByUsernameAsync(username);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return userRepository.GetAllUsersAsync();
    }

    public Task<User> UpdateUserAsync(UserDto user)
    {
        return userRepository.UpdateUserAsync(user);
    }

    public Task<User> DeleteUserAsync(Guid id)
    {
        return userRepository.DeleteUserAsync(id);
    }
}