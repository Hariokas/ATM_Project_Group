using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories;

public interface IUserRepository
{
	Task<User> AddUserAsync(User user);
	Task<User> GetUserbyIdAsync(int id);
	Task<User> GetUserbyUsernameAsync(string username);
	Task<IEnumerable<User>> GetAllUsersAsync();
	Task<IEnumerable<User>> GetUsers(int skip, int take);
	Task<User> UpdateUserAsync(User user);
	Task<User> DeleteUserAsync(Guid id);
}
