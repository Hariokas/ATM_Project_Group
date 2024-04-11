using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<Account> AddAccountAsync(Account account)
    {
        if (account is null)
            throw new ArgumentNullException(nameof(account), "Account cannot be null");

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
        var existingAccount = await GetAccountByIdAsync(account.Id);
        if (existingAccount == null)
        {
            return null;
        }
        else
        {
            context.Entry(existingAccount).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return existingAccount;
        }
    }

    public async Task<Account> DeleteAccountAsync(Guid id)
    {
        var account = await GetAccountByIdAsync(id);
        if (account == null)
        {
            return null; 
        }
        else
        {
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return account;
        }
    }
}