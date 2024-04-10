using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class TransactionService(
    ILogger<TransactionService> logger,
    ITransactionRepository transactionRepository,
    IBalanceService balanceService)
    : ITransactionService
{
    public async Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount)
    {
        try
        {
            var senderAccountBalance = await balanceService.HasEnoughMoneyAsync(sender, amount);
            if (senderAccountBalance)
            {
                sender.Balance -= amount;
                receiver.Balance += amount;

                var transaction = new Transaction
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    FromAccount = sender,
                    FromAccountId = sender.Id,
                    ToAccount = receiver,
                    ToAccountId = receiver.Id,
                    Type = TransactionType.Transfer
                };
                await transactionRepository.AddTransactionAsync(transaction);

                return transaction;
            }

            return null;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while transferring money!");
            throw;
        }
    }

    public async Task<Transaction> DepositMoney(Account account, decimal amount)
    {
        try
        {
            account.Balance += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.Now,
                ToAccount = account,
                ToAccountId = account.Id,
                Type = TransactionType.Deposit
            };
            await transactionRepository.AddTransactionAsync(transaction);

            return transaction;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while depositing money!");
            throw;
        }
    }

    public async Task<Transaction> WithdrawMoney(Account account, decimal amount)
    {
        try
        {
            var accountBalance = await balanceService.HasEnoughMoneyAsync(account, amount);
            if (accountBalance)
            {
                account.Balance -= amount;

                var transaction = new Transaction
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    FromAccount = account,
                    FromAccountId = account.Id,
                    Type = TransactionType.Withdrawal
                };
                await transactionRepository.AddTransactionAsync(transaction);

                return transaction;
            }

            return null;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while withdrawing money!");
            throw;
        }
    }
}