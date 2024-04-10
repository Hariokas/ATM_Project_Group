using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories.Interfaces;
using ATMProjectGroup.Services.Interfaces;
using System.Security.Principal;

namespace ATMProjectGroup.Services;

public class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBalanceService _balanceService;

    public TransactionService(ILogger<TransactionService> logger, ITransactionRepository transactionRepository, IBalanceService balanceService)
    {
        _logger = logger;
        _transactionRepository = transactionRepository;
        _balanceService = balanceService;
    }

    public async Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount)
    {
        try
        {
            var senderAccountBalance = await _balanceService.HasEnoughMoneyAsync(sender, amount);
            if (senderAccountBalance)
            {
                sender.Balance -= amount;
                receiver.Balance += amount;

                var transaction = new Transaction()
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    FromAccount = sender,
                    FromAccountId = sender.Id,
                    ToAccount = receiver,
                    ToAccountId = receiver.Id,
                    Type = TransactionType.Transfer
                };

                return transaction;
            }
            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while transferring money!");
            throw;
        }
        
    }

    public async Task<Transaction> DepositMoney(Account account, decimal amount)
    {
        try
        {
            account.Balance += amount;

            var transaction = new Transaction()
            {
                Amount = amount,
                TransactionDate = DateTime.Now,
                ToAccount = account,
                ToAccountId = account.Id,
                Type = TransactionType.Deposit
            };
            await _transactionRepository.AddTransactionAsync(transaction);

            return transaction;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while depositing money!");
            throw;
        }
    }
    
    public async Task<Transaction> WithdrawMoney(Account account, decimal amount)
    {
        try
        {
            var accountBalance = await _balanceService.HasEnoughMoneyAsync(account, amount);
            if (accountBalance)
            {
                account.Balance -= amount;

                var transaction = new Transaction()
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    FromAccount = account,
                    FromAccountId = account.Id,
                    Type = TransactionType.Withdrawal
                };
                await _transactionRepository.AddTransactionAsync(transaction);

                return transaction;
            }
            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while withdrawing money!");
            throw;
        }
    }
}