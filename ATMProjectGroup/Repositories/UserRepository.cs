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
            throw;
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
            throw;
        }
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentNullException(nameof(username));
        }

        try
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Username == username) ??
                   throw new NullReferenceException(
                       $"[{nameof(GetUserByUsernameAsync)}]: User with username [{username}] not found!");
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(GetUserByUsernameAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await db.Users.ToListAsync();
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(GetAllUsersAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetUsers(int skip, int take)
    {
        try
        {
            return await db.Users.Skip(skip).Take(take).ToListAsync();
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(GetUsers)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        try
        {
            db.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(UpdateUserAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> DeleteUserAsync(Guid id)
    {
        try
        {
            var user = await GetUserByIdAsync(id);
            ArgumentNullException.ThrowIfNull(user);

            db.Remove(user);
            await db.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            //Log.Error($"[{nameof(DeleteUserAsync)}]: {e.Message}");
            throw;
        }
    }
}