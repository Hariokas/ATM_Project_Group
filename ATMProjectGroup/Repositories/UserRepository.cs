using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<User> AddUserAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        try
        {
            db.Add(user);
            await db.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(AddUserAsync)}]: {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id) ??
                   throw new NullReferenceException($"[{nameof(GetUserByIdAsync)}]: User with GUID [{id}] not found!");
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(GetUserByIdAsync)}]: {e.Message}");
            throw new Exception(e.Message);
        }
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