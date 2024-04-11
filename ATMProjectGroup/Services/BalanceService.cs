using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class BalanceService(IAccountRepository accountRepository) : IBalanceService
{
    public async Task<decimal> GetBalanceAsync(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        var retrievedAccount = await accountRepository.GetAccountByIdAsync(account.Id);
        if (retrievedAccount == null) throw new KeyNotFoundException($"No account found with ID {account.Id}");

        return retrievedAccount.Balance;
        //throw new NotImplementedException();
    }

    public async Task<bool> HasEnoughMoneyAsync(Account account, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative.");

        var currentBalance = await GetBalanceAsync(account);
        return currentBalance >= amount;
        //throw new NotImplementedException();
    }
}