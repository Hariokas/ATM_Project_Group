using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.EF;
using ATMProjectGroup.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public async Task<Transaction> AddTransactionAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> GetTransactionByIdAsync(Guid id)
    {
        return await context.Transactions.FirstOrDefaultAsync(t => t.Id == id);

    }

    public async Task<IEnumerable<Transaction>> GetTransactionsFromUser(Guid userId)
    {
        return await context.Transactions
            .Where(t => t.FromAccount.UserId == userId || t.ToAccount.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsFromAccount(Guid accountId)
    {
        return await context.Transactions
            .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
            .ToListAsync();
    }

    public async Task<Transaction> UpdateTransactionAsync(/*Guid id*/Transaction transaction) //shouldn't we state some ID to locate the object?
    {
        var existingTransaction = context.Transactions.FirstOrDefault(t => t.Id == transaction.Id /*==id*/);
        //existingTransaction.Amount = transaction.Amount;
        //existingTransaction.TransactionDate = transaction.TransactionDate;
        //existingTransaction.Description = transaction.Description;
        //existingTransaction.FromAccountId = transaction.FromAccountId;
        //existingTransaction.FromAccount = transaction.FromAccount;
        //existingTransaction.ToAccountId = transaction.ToAccountId;
        //existingTransaction.ToAccount = transaction.ToAccount;
        //existingTransaction.Type = transaction.Type;

        context.Transactions.Update(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> DeleteTransactionAsync(Guid id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
        }
        return transaction;

    }
}