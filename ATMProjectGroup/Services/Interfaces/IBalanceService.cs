using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces;

public interface IBalanceService
{
    Task<decimal> GetBalanceAsync(Account account);
    Task<bool> HasEnoughMoneyAsync(Account account, decimal amount);
}