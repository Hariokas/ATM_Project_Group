using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<Account> AddAccountAsync(Account account)
    {
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> GetAccountByIdAsync(Guid id)
    {
        return await context.Accounts.FindAsync(id);
    }

    public async Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId)
    {
        return await context.Accounts.Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        context.Entry(account).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> DeleteAccountAsync(Guid id)
    {
        var account = await context.Accounts.FindAsync(id);
        if (account != null) return null;
        context.Accounts.Remove(account);
        await context.SaveChangesAsync();
        return account;
    }
}