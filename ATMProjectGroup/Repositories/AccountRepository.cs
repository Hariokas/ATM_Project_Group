using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;

namespace ATMProjectGroup.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task<Account> AddAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Account> UpdateAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> DeleteAccountAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}