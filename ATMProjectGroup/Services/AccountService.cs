using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> AddAccountAsync(Account account)
    {
        return await _accountRepository.AddAccountAsync(account);
    }

    public async Task<Account> GetAccountByIdAsync(Guid id)
    {
        return await _accountRepository.GetAccountByIdAsync(id);
    }

    public async Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId)
    {
        return await _accountRepository.GetAccountsFromUser(userId);
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        return await _accountRepository.UpdateAccountAsync(account);
    }

    public async Task<Account> DeleteAccountAsync(Guid id)
    {
        return await _accountRepository.DeleteAccountAsync(id);
    }

}