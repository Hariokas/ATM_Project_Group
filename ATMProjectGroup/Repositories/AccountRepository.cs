using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Account> AddAccountAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> GetAccountByIdAsync(Guid id)
    {
        return await _context.Accounts.FindAsync(id);
    }
    public async Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId)
    {
        return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> DeleteAccountAsync(Guid id)
    {
       var account = await _context.Accounts.FindAsync(id);
       if (account != null)
        {
            return null;
        }
       _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return account;
    }
}