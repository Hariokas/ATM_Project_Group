using ATMProjectGroup.Models;

namespace ATMProjectGroup.Services;

public interface ITransactionService
{
	Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount);
}
