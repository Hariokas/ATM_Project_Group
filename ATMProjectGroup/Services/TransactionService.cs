using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;

namespace ATMProjectGroup.Services;

public class TransactionService(
    ILogger<TransactionService> logger,
    ITransactionRepository transactionRepository,
    IAccountRepository accountRepository,
    IBalanceService balanceService)
    : ITransactionService
{
    public async Task<TransactionResultDto> TransferMoney(Guid senderAccountId, Guid receiverAccountId, decimal amount)
    {
        try
        {
            var sender = await accountRepository.GetAccountByIdAsync(senderAccountId);
            var receiver = await accountRepository.GetAccountByIdAsync(receiverAccountId);

            if (sender == null || receiver == null)
                throw new ArgumentException("One of the accounts is not found.");

            var senderAccountBalance = await balanceService.HasEnoughMoneyAsync(sender, amount);
            if (senderAccountBalance)
            {
                sender.Balance -= amount;
                receiver.Balance += amount;

                await accountRepository.UpdateAccountAsync(sender);
                await accountRepository.UpdateAccountAsync(receiver);

                var transaction = new Transaction
                {
                    Amount = amount,
                    TransactionDate = DateTime.UtcNow,
                    FromAccountId = sender.Id,
                    ToAccountId = receiver.Id,
                    Type = TransactionType.Transfer,
                    Description = "Transfer from account " + sender.AccountNumber + " to account " +
                                  receiver.AccountNumber
                };

                await transactionRepository.AddTransactionAsync(transaction);

                return new TransactionResultDto
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    Description = transaction.Description,
                    Type = transaction.Type
                };
            }

            throw new InvalidOperationException("Insufficient funds for the transfer.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while transferring money between accounts.");
            throw;
        }
    }

    public async Task<TransactionResultDto> DepositMoney(Guid accountId, decimal amount)
    {
        try
        {
            var account = await accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");

            account.Balance += amount;
            await accountRepository.UpdateAccountAsync(account);

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                ToAccountId = account.Id,
                Type = TransactionType.Deposit
            };
            await transactionRepository.AddTransactionAsync(transaction);

            return new TransactionResultDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = "Deposit into account " + account.AccountNumber,
                Type = transaction.Type
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while depositing money!");
            throw;
        }
    }

    public async Task<TransactionResultDto> WithdrawMoney(Guid accountId, decimal amount)
    {
        try
        {
            var account = await accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");

            var accountBalance = await balanceService.HasEnoughMoneyAsync(account, amount);
            if (!accountBalance)
                throw new InvalidOperationException("Insufficient funds for the withdrawal.");

            account.Balance -= amount;
            await accountRepository.UpdateAccountAsync(account);

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                FromAccountId = account.Id,
                Type = TransactionType.Withdrawal
            };
            await transactionRepository.AddTransactionAsync(transaction);

            return new TransactionResultDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = "Withdrawal from account " + account.AccountNumber,
                Type = transaction.Type
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while withdrawing money!");
            throw;
        }
    }
}