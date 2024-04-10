using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ATMProjectGroup.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        int savedChangesValue = await _context.SaveChangesAsync();
        if (savedChangesValue <= 0)
        {
            throw new InvalidOperationException("Error saving changes to the database!");
        }
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