using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces;

public interface ITransactionService
{
    Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount);
    Task<Transaction> DepositMoney(Account account, decimal amount);
    Task<Transaction> WithdrawMoney(Account account, decimal amount);
}