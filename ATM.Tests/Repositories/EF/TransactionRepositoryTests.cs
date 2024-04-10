using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories;
using ATMProjectGroup.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace ATM.Tests.Repositories.EF;

public class TransactionRepositoryTests
{
	[Fact]
	public async Task AddTransactionAsync_TransactionNotNull_ShouldReturnTransaction()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddTransactionAsync_ShouldReturnTransaction")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var transaction = new Transaction
			{
				Id = Guid.NewGuid(),	
				FromAccountId = Guid.NewGuid(),
				ToAccountId = Guid.NewGuid(),
				Amount = 1000,
				Type = TransactionType.Deposit
			};

			// Act
			var result = await transactionRepository.AddTransactionAsync(transaction);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(transaction.Id, result.Id);
			Assert.Equal(transaction.FromAccountId, result.FromAccountId);
			Assert.Equal(transaction.ToAccountId, result.ToAccountId);
			Assert.Equal(transaction.Amount, result.Amount);
			Assert.Equal(transaction.Type, result.Type);
		}
	}

	[Fact]
	public async Task AddTransactionAsync_TransactionNull_ShouldThrowArgumentNullException()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddTransactionAsync_ShouldThrowArgumentNullException")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();

			// Act
			async Task AddTransactionAsync() => await transactionRepository.AddTransactionAsync(null!);

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(AddTransactionAsync);
		}
	}

	[Fact]
	public async Task GetTransactionByIdAsync_TransactionExists_ShouldReturnTransaction()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionByIdAsync_ShouldReturnTransaction")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var transaction = new Transaction
			{
				Id = Guid.NewGuid(),
				FromAccountId = Guid.NewGuid(),
				ToAccountId = Guid.NewGuid(),
				Amount = 1000,
				Type = TransactionType.Deposit
			};

			await transactionRepository.AddTransactionAsync(transaction);

			// Act
			var result = await transactionRepository.GetTransactionByIdAsync(transaction.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(transaction.Id, result.Id);
			Assert.Equal(transaction.FromAccountId, result.FromAccountId);
			Assert.Equal(transaction.ToAccountId, result.ToAccountId);
			Assert.Equal(transaction.Amount, result.Amount);
		}
	}

	[Fact]
	public async Task GetTransactionByIdAsync_TransactionDoesNotExist_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionByIdAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();

			// Act
			var result = await transactionRepository.GetTransactionByIdAsync(Guid.NewGuid());

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task GetTransactionsFromUser_UserExists_ShouldReturnTransactions()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionsFromUser_ShouldReturnTransactions")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var userRepository = new UserRepository();
			var userId = Guid.NewGuid();
			var user = new User
			{
				Id = userId,
				Accounts = new List<Account>
				{
					new() {
						Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
						UserId = userId,
						Balance = 1000,
						AccountNumber = "1234567890",
						OutgoingTransactions = new List<Transaction>()
						{
							new()
							{
								Id = Guid.NewGuid(),
								FromAccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
								ToAccountId = Guid.NewGuid(),
								Amount = 1000,
								Type = TransactionType.Transfer,
							},
							new()
							{
								Id = Guid.NewGuid(),
								FromAccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
								ToAccountId = Guid.NewGuid(),
								Amount = 10,
								Type = TransactionType.Transfer,
							}
						}
					}
				}
			};

			await userRepository.AddUserAsync(user);

			// Act
			var result = await transactionRepository.GetTransactionsFromUser(userId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Accounts.SelectMany(a => a.OutgoingTransactions).Count(), result.Count());
		}
	}

	[Fact]
	public async Task GetTransactionsFromUser_UserNotExists_ShouldReturnEmpty()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionsFromUser_ShouldReturnEmpty")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();

			// Act
			var result = await transactionRepository.GetTransactionsFromUser(Guid.NewGuid());

			// Assert
			Assert.Empty(result);
		}
	}

	[Fact]
	public async Task GetTransactionsFromAccount_AccountExists_ShouldReturnTransactions()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionsFromAccount_ShouldReturnTransactions")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var accountRepository = new AccountRepository();
			var accountId = Guid.NewGuid();
			var account = new Account
			{
				Id = accountId,
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890",
				OutgoingTransactions = new List<Transaction>
				{
					new()
					{
						Id = Guid.NewGuid(),
						FromAccountId = accountId,
						ToAccountId = Guid.NewGuid(),
						Amount = 1000,
						Type = TransactionType.Transfer,
					},
					new()
					{
						Id = Guid.NewGuid(),
						FromAccountId = accountId,
						ToAccountId = Guid.NewGuid(),
						Amount = 10,
						Type = TransactionType.Transfer,
					}
				}
			};

			await accountRepository.AddAccountAsync(account);

			// Act
			var result = await transactionRepository.GetTransactionsFromAccount(accountId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(account.OutgoingTransactions.Count, result.Count());
		}
	}

	[Fact]
	public async Task GetTransactionsFromAccount_AccountNotExists_ShouldReturnEmpty()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetTransactionsFromAccount_ShouldReturnEmpty")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();

			// Act
			var result = await transactionRepository.GetTransactionsFromAccount(Guid.NewGuid());

			// Assert
			Assert.Empty(result);
		}
	}

	[Fact]
	public async Task UpdateTransactionAsync_TransactionExists_ShouldReturnUpdatedTransaction()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateTransactionAsync_ShouldReturnUpdatedTransaction")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var transaction = new Transaction
			{
				Id = Guid.NewGuid(),
				FromAccountId = Guid.NewGuid(),
				ToAccountId = Guid.NewGuid(),
				Amount = 1000,
				Type = TransactionType.Deposit
			};

			await transactionRepository.AddTransactionAsync(transaction);

			transaction.Amount = 2000;

			// Act
			var result = await transactionRepository.UpdateTransactionAsync(transaction);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(transaction.Id, result.Id);
			Assert.Equal(transaction.FromAccountId, result.FromAccountId);
			Assert.Equal(transaction.ToAccountId, result.ToAccountId);
			Assert.Equal(transaction.Amount, result.Amount);
			Assert.Equal(transaction.Type, result.Type);
		}
	}

	[Fact]
	public async Task UpdateTransactionAsync_TransactionNotExists_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateTransactionAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var transaction = new Transaction
			{
				Id = Guid.NewGuid(),
				FromAccountId = Guid.NewGuid(),
				ToAccountId = Guid.NewGuid(),
				Amount = 1000,
				Type = TransactionType.Deposit
			};

			// Act
			var result = await transactionRepository.UpdateTransactionAsync(transaction);

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task DeleteTransactionAsync_TransactionExists_ShouldReturnDeletedTransaction()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteTransactionAsync_ShouldReturnDeletedTransaction")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();
			var transaction = new Transaction
			{
				Id = Guid.NewGuid(),
				FromAccountId = Guid.NewGuid(),
				ToAccountId = Guid.NewGuid(),
				Amount = 1000,
				Type = TransactionType.Deposit
			};

			await transactionRepository.AddTransactionAsync(transaction);

			// Act
			var result = await transactionRepository.DeleteTransactionAsync(transaction.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(transaction.Id, result.Id);
			Assert.Equal(transaction.FromAccountId, result.FromAccountId);
			Assert.Equal(transaction.ToAccountId, result.ToAccountId);
			Assert.Equal(transaction.Amount, result.Amount);
			Assert.Equal(transaction.Type, result.Type);
		}
	}

	[Fact]
	public async Task DeleteTransactionAsync_TransactionNotExists_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteTransactionAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var transactionRepository = new TransactionRepository();

			// Act
			var result = await transactionRepository.DeleteTransactionAsync(Guid.NewGuid());

			// Assert
			Assert.Null(result);
		}
	}
}
