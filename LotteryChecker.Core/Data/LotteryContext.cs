using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Data;

public class LotteryContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
	public DbSet<Lottery> Lotteries { get; set; }
	public DbSet<Reward> Rewards { get; set; }
	public DbSet<SearchHistory> SearchHistories { get; set; }
	public DbSet<PurchaseTicket> PurchaseTickets { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected LotteryContext()
	{
	}

	public LotteryContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		string connectionString = "Server=DESKTOP-38FHDTU\\SQLEXPRESS;Database=FSA_Lottery;uid=sa;pwd=12345678;Trusted_Connection=true;TrustServerCertificate=True; MultipleActiveResultSets=true";
		base.OnConfiguring(optionsBuilder);
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
    
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Seed();
		base.OnModelCreating(builder);

		builder.Entity<Lottery>().Navigation<Reward>(l => l.Reward).AutoInclude();
		builder.Entity<SearchHistory>().Navigation(l => l.User).AutoInclude();
	}
}