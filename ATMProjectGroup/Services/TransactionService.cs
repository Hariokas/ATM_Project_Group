using ATMProjectGroup.Models;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class TransactionService : ITransactionService
{
    public Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> DepositMoney(Account account, decimal amount)
    {
        throw new NotImplementedException();
    }
    
    public Task<Transaction> WithdrawMoney(Account account, decimal amount)
    {
        throw new NotImplementedException();
    }
}