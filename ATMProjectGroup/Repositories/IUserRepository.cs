using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories;

public interface IUserRepository
{
	Task<User> AddUserAsync(User user);
	Task<User> GetUserByIdAsync(int id);
	Task<User> GetUserByUsernameAsync(string username);
	Task<IEnumerable<User>> GetAllUsersAsync();
	Task<IEnumerable<User>> GetUsers(int skip, int take);
	Task<User> UpdateUserAsync(User user);
	Task<User> DeleteUserAsync(Guid id);
}
