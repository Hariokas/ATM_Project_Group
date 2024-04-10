using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task AddTransactionAsync(Transaction transaction);
    Task<Transaction> GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetTransactionsFromUser(Guid userId);
    Task<IEnumerable<Transaction>> GetTransactionsFromAccount(Guid accountId);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task<Transaction> DeleteTransactionAsync(Guid id);
}