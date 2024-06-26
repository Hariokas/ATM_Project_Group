﻿using ATMProjectGroup.Models;
using ATMProjectGroup.Repositories;
using ATMProjectGroup.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace ATM.Tests.Repositories.EF;

public class UserRepositoryTests
{
	[Fact]
	public async Task AddUserAsync_UserNotNull_ShouldReturnUser()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddUserAsync_ShouldReturnUser")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			// Act
			var result = await userRepository.AddUserAsync(user);

			Assert.NotNull(result);
			Assert.Equal(user.Id, result.Id);
			Assert.Equal(user.Username, result.Username);
			Assert.Equal(user.PasswordHash, result.PasswordHash);
		}
	}

	[Fact]
	public async Task AddUserAsync_UserNull_ShouldThrowArgumentNullException()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "AddUserAsync_ShouldThrowArgumentNullException")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();

			// Act
			async Task AddUserAsync() => await userRepository.AddUserAsync(null!);

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(AddUserAsync);
		}
	}

	[Fact]
	public async Task GetUserByIdAsync_UserExists_ShouldReturnUser()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUserByIdAsync_ShouldReturnUser")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			await userRepository.AddUserAsync(user);

			// Act
			var result = await userRepository.GetUserByIdAsync(0);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Id, result.Id);
			Assert.Equal(user.Username, result.Username);
			Assert.Equal(user.PasswordHash, result.PasswordHash);
		}
	}

	[Fact]
	public async Task GetUserByIdAsync_UserDoesNotExist_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUserByIdAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();

			// Act
			var result = await userRepository.GetUserByIdAsync(0);

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task GetUserByUsernameAsync_UserExists_ShouldReturnUser()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUserByUsernameAsync_ShouldReturnUser")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			await userRepository.AddUserAsync(user);

			// Act
			var result = await userRepository.GetUserByUsernameAsync("test");

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Id, result.Id);
			Assert.Equal(user.Username, result.Username);
			Assert.Equal(user.PasswordHash, result.PasswordHash);
		}
	}

	[Fact]
	public async Task GetUserByUsernameAsync_UserDoesNotExist_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUserByUsernameAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();

			// Act
			var result = await userRepository.GetUserByUsernameAsync("test");

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task GetAllUsersAsync_UsersExist_ShouldReturnUsers()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAllUsersAsync_ShouldReturnUsers")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user1 = new User
			{
				Id = Guid.NewGuid(),
				Username = "test1",
				PasswordHash = "test1"
			};
			var user2 = new User
			{
				Id = Guid.NewGuid(),
				Username = "test2",
				PasswordHash = "test2"
			};

			await userRepository.AddUserAsync(user1);
			await userRepository.AddUserAsync(user2);

			// Act
			var result = await userRepository.GetAllUsersAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			Assert.Contains(result, u => u.Id == user1.Id);
			Assert.Contains(result, u => u.Id == user2.Id);
		}
	}

	[Fact]
	public async Task GetAllUsersAsync_NoUsersExist_ShouldReturnEmpty()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAllUsersAsync_ShouldReturnEmpty")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();

			// Act
			var result = await userRepository.GetAllUsersAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}

	[Fact]
	public async Task GetUsers_UsersExist_ShouldReturnUsers()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUsers_ShouldReturnUsers")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user1 = new User
			{
				Id = Guid.NewGuid(),
				Username = "test1",
				PasswordHash = "test1"
			};
			var user2 = new User
			{
				Id = Guid.NewGuid(),
				Username = "test2",
				PasswordHash = "test2"
			};

			await userRepository.AddUserAsync(user1);
			await userRepository.AddUserAsync(user2);

			// Act
			var result = await userRepository.GetUsers(0, 2);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			Assert.Contains(result, u => u.Id == user1.Id);
			Assert.Contains(result, u => u.Id == user2.Id);
		}
	}

	[Fact]
	public async Task GetUsers_NoUsersExist_ShouldReturnEmpty()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "GetUsers_ShouldReturnEmpty")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();

			// Act
			var result = await userRepository.GetUsers(0, 2);

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}

	[Fact]
	public async Task UpdateUserAsync_UserExists_ShouldReturnUpdatedUser()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateUserAsync_ShouldReturnUpdatedUser")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			await userRepository.AddUserAsync(user);

			var updatedUser = new User
			{
				Id = user.Id,
				Username = "test2",
				PasswordHash = "test2"
			};

			// Act
			var result = await userRepository.UpdateUserAsync(updatedUser);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(updatedUser.Id, result.Id);
			Assert.Equal(updatedUser.Username, result.Username);
			Assert.Equal(updatedUser.PasswordHash, result.PasswordHash);
		}
	}

	[Fact]
	public async Task UpdateUserAsync_UserDoesNotExist_ShouldReturnNull()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateUserAsync_ShouldReturnNull")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			// Act
			var result = await userRepository.UpdateUserAsync(user);

			// Assert
			Assert.Null(result);
		}
	}

	[Fact]
	public async Task DeleteUserAsync_UserExists_ShouldReturnDeletedUser()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteUserAsync_ShouldReturnDeletedUser")
			.Options;

		using (var context = new AppDbContext(options))
		{
			var userRepository = new UserRepository();
			var user = new User
			{
				Id = Guid.NewGuid(),
				Username = "test",
				PasswordHash = "test"
			};

			await userRepository.AddUserAsync(user);

			// Act
			var result = await userRepository.DeleteUserAsync(user.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Id, result.Id);
			Assert.Equal(user.Username, result.Username);
			Assert.Equal(user.PasswordHash, result.PasswordHash);
		}
	}
}