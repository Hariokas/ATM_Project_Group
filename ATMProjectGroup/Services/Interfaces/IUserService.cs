using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(Guid id);
    }
}
