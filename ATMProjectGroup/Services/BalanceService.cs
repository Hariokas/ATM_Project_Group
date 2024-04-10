using ATMProjectGroup.Models;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class BalanceService : IBalanceService
{
    public Task<decimal> GetBalanceAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasEnoughMoneyAsync(Account account, decimal amount)
    {
        throw new NotImplementedException();
    }
}