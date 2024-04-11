using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces;

public interface IAccountService
{
    Task<Account> AddAccountAsync(Account account);
    Task<Account> GetAccountByIdAsync(Guid id);
    Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId);
    Task<Account> UpdateAccountAsync(Account account);
    Task<Account> DeleteAccountAsync(Guid id);
}