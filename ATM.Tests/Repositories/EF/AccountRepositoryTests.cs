using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories;
using ATMProjectGroup.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace ATM.Tests.Repositories.EF;

public class AccountRepositoryTests
{
	[Fact]
	public async Task AddAccountAsync_AccountNotNull_ShouldReturnAccount()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddAccountAsync_ShouldReturnAccount")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var account = new Account
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890"
			};

			// Act
			var result = await accountRepository.AddAccountAsync(account);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(account.Id, result.Id);
			Assert.Equal(account.UserId, result.UserId);
			Assert.Equal(account.Balance, result.Balance);
		}
	}

	[Fact]
	public async Task AddAccountAsync_AccountNull_ShouldThorwArgumentNullException()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddAccountAsync_ShouldThrowArgumentNullException")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();

			// Act
			async Task AddAccountAsync() => await accountRepository.AddAccountAsync(null!);

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(AddAccountAsync);
		}
	}

	[Fact]
	public async Task GetAccountByIdAsync_AccountExists_ShouldReturnAccount()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAccountByIdAsync_ShouldReturnAccount")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var account = new Account
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890"
			};

			await accountRepository.AddAccountAsync(account);

			// Act
			var result = await accountRepository.GetAccountByIdAsync(account.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(account.Id, result.Id);
			Assert.Equal(account.UserId, result.UserId);
			Assert.Equal(account.Balance, result.Balance);
		}
	}

	[Fact]
	public async Task GetAccountByIdAsync_AccountNotExists_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAccountByIdAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();

			// Act
			var result = await accountRepository.GetAccountByIdAsync(Guid.NewGuid());

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task GetAccountsFromUser_UserExists_ShouldReturnAccounts()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAccountsFromUser_ShouldReturnAccounts")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var userId = Guid.NewGuid();
			var account1 = new Account
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				Balance = 1000,
				AccountNumber = "1234567890"
			};
			var account2 = new Account
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				Balance = 2000,
				AccountNumber = "0987654321"
			};

			await accountRepository.AddAccountAsync(account1);
			await accountRepository.AddAccountAsync(account2);

			// Act
			var result = await accountRepository.GetAccountsFromUser(userId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
		}
	}

	[Fact]
	public async Task GetAccountsFromUser_UserNotExists_ShouldReturnEmpty()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAccountsFromUser_ShouldReturnEmpty")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();

			// Act
			var result = await accountRepository.GetAccountsFromUser(Guid.NewGuid());

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}

	[Fact]
	public async Task UpdateAccountAsync_AccountExists_ShouldReturnUpdatedAccount()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateAccountAsync_ShouldReturnUpdatedAccount")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var account = new Account
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890"
			};

			await accountRepository.AddAccountAsync(account);

			account.Balance = 2000;

			// Act
			var result = await accountRepository.UpdateAccountAsync(account);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(account.Id, result.Id);
			Assert.Equal(account.UserId, result.UserId);
			Assert.Equal(account.Balance, result.Balance);
		}
	}

	[Fact]
	public async Task UpdateAccountAsync_AccountNotExists_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateAccountAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var account = new Account
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890"
			};

			// Act
			var result = await accountRepository.UpdateAccountAsync(account);

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task DeleteAccountAsync_AccountExists_ShouldReturnDeletedAccount()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteAccountAsync_ShouldReturnDeletedAccount")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();
			var account = new Account
			{
				Id = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Balance = 1000,
				AccountNumber = "1234567890"
			};

			await accountRepository.AddAccountAsync(account);

			// Act
			var result = await accountRepository.DeleteAccountAsync(account.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(account.Id, result.Id);
			Assert.Equal(account.UserId, result.UserId);
			Assert.Equal(account.Balance, result.Balance);
		}
	}

	[Fact]
	public async Task DeleteAccountAsync_AccountNotExists_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteAccountAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var accountRepository = new AccountRepository();

			// Act
			var result = await accountRepository.DeleteAccountAsync(Guid.NewGuid());

			// Assert
			Assert.Null(result);
		}
	}
}
