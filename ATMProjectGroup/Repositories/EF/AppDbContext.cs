using ATMProjectGroup.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMProjectGroup.Repositories.EF;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasData(
				new User
				{
					Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
					Username = "ATM Withdrawal",
					PasswordHash = string.Empty,
				},
				new User
				{
					Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
					Username = "ATM Deposit",
					PasswordHash = string.Empty,
				}
			);

		modelBuilder.Entity<Account>()
			.HasData(
				new Account
				{
					Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
					AccountNumber = "0000000001",
					Balance = 10_000,
					UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
				},
				new Account
				{
					Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
					AccountNumber = "0000000002",
					Balance = 10_000,
					UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
				}
														);

		modelBuilder.Entity<Transaction>()
			.HasOne(t => t.FromAccount)
			.WithMany(a => a.OutgoingTransactions)
			.HasForeignKey(t => t.FromAccountId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Transaction>()
			.HasOne(t => t.ToAccount)
			.WithMany(a => a.IncomingTransactions)
			.HasForeignKey(t => t.ToAccountId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Account>()
			.HasOne(a => a.User)
			.WithMany(u => u.Accounts)
			.HasForeignKey(a => a.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
