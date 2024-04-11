using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionResultDto> TransferMoney(Guid senderAccountId, Guid receiverAccountId, decimal amount);
    Task<TransactionResultDto> DepositMoney(Guid accountId, decimal amount);
    Task<TransactionResultDto> WithdrawMoney(Guid accountId, decimal amount);
}