using ATMProjectGroup.Models;

namespace ATMProjectGroup.Repositories;

public interface IAccountRepository
{
	Task<Account> AddAccountAsync(Account account);
	Task<Account> GetAccountbyIdAsync(Guid id);
	Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId);
	Task<Account> UpdateAccountAsync(Account account);
	Task<Account> DeleteAccountAsync(Guid id);
}
