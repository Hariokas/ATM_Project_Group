using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;

namespace ATMProjectGroup.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task<Transaction> AddTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> GetTransactionByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetTransactionsFromUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetTransactionsFromAccount(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> DeleteTransactionAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}