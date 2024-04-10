using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> AddTransactionAsync(Transaction transaction);
    Task<Transaction> GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetTransactionsFromUser(Guid userId);
    Task<IEnumerable<Transaction>> GetTransactionsFromAccount(Guid accountId);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task<Transaction> DeleteTransactionAsync(Guid id);
}