using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Data;
public static class LotteryInitializer
{
	public static void Seed(this ModelBuilder builder)
	{
        builder.Entity<Lottery>().HasData(
            new Lottery
            {
                LotteryId = 1,
                DueDate = new DateTime(2024, 5, 10),
                PublishDate = null, 
                LotteryNumber = 123456,
                RewardId = 1 
            },
            new Lottery
            {
                LotteryId = 2,
                DueDate = new DateTime(2024, 5, 11),
                PublishDate = null, 
                LotteryNumber = 234567,
                RewardId = 2 
            },
            new Lottery
            {
                LotteryId = 3,
                DueDate = new DateTime(2024, 5, 12),
                PublishDate = null, 
                LotteryNumber = 345678,
                RewardId = 3 
            },
            new Lottery
            {
                LotteryId = 4,
                DueDate = new DateTime(2024, 5, 13),
                PublishDate = null,
                LotteryNumber = 456789,
                RewardId = 4 
            },
            new Lottery
            {
                LotteryId = 5,
                DueDate = new DateTime(2024, 5, 14),
                PublishDate = null, 
                LotteryNumber = 567890,
                RewardId = 5 
            },
            new Lottery
            {
                LotteryId = 6,
                DueDate = new DateTime(2024, 5, 15),
                PublishDate = null,
                LotteryNumber = 678901,
                RewardId = 6
            }
        );

        builder.Entity<Reward>().HasData(
            new Reward { RewardId = 1, RewardValue = 1000000,RewardName = "Prize 8" },
            new Reward { RewardId = 2, RewardValue = 2000000,RewardName = "Prize 7" },
            new Reward { RewardId = 3, RewardValue = 3000000,RewardName = "Prize 6" },
            new Reward { RewardId = 4, RewardValue = 4000000,RewardName = "Prize 5" },
            new Reward { RewardId = 5, RewardValue = 5000000,RewardName = "Prize 4" },
            new Reward { RewardId = 6, RewardValue = 6000000,RewardName = "Prize 3" },
            new Reward { RewardId = 7, RewardValue = 7000000,RewardName = "Prize 2" },
            new Reward { RewardId = 8, RewardValue = 8000000,RewardName = "Prize 1" }
        );

        builder.Entity<SearchHistory>().HasData(
            new SearchHistory
            {
                SearchHistoryId = 1,
                SearchDate = new DateTime(2024, 5, 10),
                LotteryNumber = 123456,
                UserId = new Guid("36B35306-154C-4518-8FC1-D7E756522111")
            },
            new SearchHistory
            {
                SearchHistoryId = 2,
                SearchDate = new DateTime(2024, 5, 11),
                LotteryNumber = 234567,
                UserId = new Guid("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D")
            },
            new SearchHistory
            {
                SearchHistoryId = 3,
                SearchDate = new DateTime(2024, 5, 12),
                LotteryNumber = 345678,
                UserId = new Guid("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D")
            }
        );
        builder.Entity<PurchaseTicket>().HasData(
                new PurchaseTicket
                {
                    PurchaseTicketId = 1,
                    PurchaseDate = new DateTime(2024, 5, 10),
                    LotteryNumber = 123456,
                    UserId = new Guid("36B35306-154C-4518-8FC1-D7E756522111")
                },
                new PurchaseTicket
                {
                    PurchaseTicketId = 2,
                    PurchaseDate = new DateTime(2024, 5, 11),
                    LotteryNumber = 234567,
                    UserId = new Guid("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D")
                },
                new PurchaseTicket
                {
                    PurchaseTicketId = 3,
                    PurchaseDate = new DateTime(2024, 5, 12),
                    LotteryNumber = 345678,
                    UserId = new Guid("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D")
                },
                new PurchaseTicket
                {
                    PurchaseTicketId =  4,
                    PurchaseDate = new DateTime(2024, 5, 13),
                    LotteryNumber = 456789,
                    UserId = new Guid("36B35306-154C-4518-8FC1-D7E756522111")
                }
        );

        builder.Entity<IdentityRole<Guid>>().HasData(
               new IdentityRole<Guid> { Id = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole<Guid> { Id = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E"), Name = "User", NormalizedName = "USER" }
        );
        builder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = Guid.Parse("24DD0B58-C0E0-470C-8ED2-14467A3B868F"),
                FirstName = "Hoang Chi",
                LastName = "Hieu",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            },
            new AppUser
            {
                Id = Guid.Parse("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D"),
                FirstName = "Ho Van",
                LastName = "Hieu",
                UserName = "hieuhv@gmail.com",
                NormalizedUserName = "HIEUHV@GMAIL.COM",
                Email = "hieuhv@gmail.com",
                NormalizedEmail = "HIEUHV@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Hieu@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            },
            new AppUser
            {
                Id = Guid.Parse("36B35306-154C-4518-8FC1-D7E756522111"),
                FirstName = "Le Quang",
                LastName = "Viet",
                UserName = "vietlq@gmail.com",
                NormalizedUserName = "VIETLQ@GMAIL.COM",
                Email = "vietlq@gmail.com",
                NormalizedEmail = "VIETLQ@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Viet@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            }
        );
        builder.Entity<IdentityUserRole<Guid>>().HasData(
            //Seed admin
            new IdentityUserRole<Guid> { UserId = Guid.Parse("24DD0B58-C0E0-470C-8ED2-14467A3B868F"), RoleId = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA") },
            //Seend user
            new IdentityUserRole<Guid> { UserId = Guid.Parse("36B35306-154C-4518-8FC1-D7E756522111"), RoleId = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E") },
            //Seend user 2
            new IdentityUserRole<Guid> { UserId = Guid.Parse("57FA9A8E-3105-49A0-B0F2-6D88FDFCFF8D"), RoleId = Guid.Parse("FE0E9C2D-6ABD-4F73-A635-63FC58EC700E") }
        );
    }
}