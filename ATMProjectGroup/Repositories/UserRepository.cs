using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;

namespace ATMProjectGroup.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsers(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}