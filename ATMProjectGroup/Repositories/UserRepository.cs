using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ATMProjectGroup.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> AddUserAsync(UserDto userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto);

        try
        {
            var user = MapUserDtoToNewUser(userDto);
            context.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(AddUserAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id) ??
                   throw new NullReferenceException($"[{nameof(GetUserByIdAsync)}]: User with GUID [{id}] not found!");
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(GetUserByIdAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));

        try
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username) ??
                   throw new NullReferenceException(
                       $"[{nameof(GetUserByUsernameAsync)}]: User with username [{username}] not found!");
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(GetUserByUsernameAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await context.Users.ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(GetAllUsersAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetUsers(int skip, int take)
    {
        try
        {
            return await context.Users.Skip(skip).Take(take).ToListAsync();
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(GetUsers)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> UpdateUserAsync(UserDto userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto);

        try
        {
            var user = MapUserDtoToNewUser(userDto);
            context.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(UpdateUserAsync)}]: {e.Message}");
            throw;
        }
    }

    public async Task<User> DeleteUserAsync(Guid id)
    {
        try
        {
            var user = await GetUserByIdAsync(id);
            ArgumentNullException.ThrowIfNull(user);

            context.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Log.Error($"[{nameof(DeleteUserAsync)}]: {e.Message}");
            throw;
        }
    }

    private User MapUserDtoToNewUser(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Username = userDto.Username,
            PasswordHash = userDto.PasswordHash,
            Accounts = new List<Account>()
        };
    }
}