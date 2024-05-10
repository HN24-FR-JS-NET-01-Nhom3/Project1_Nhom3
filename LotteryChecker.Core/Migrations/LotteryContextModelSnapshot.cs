﻿// <auto-generated />
using System;
using LotteryChecker.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LotteryChecker.Core.Migrations
{
    [DbContext(typeof(LotteryContext))]
    partial class LotteryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LotteryChecker.Core.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "49519764-bd81-439f-ba0b-60e1b0e773b9",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Hoang Chi",
                            IsActive = false,
                            LastName = "Hieu",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDmdr43JJanNhyEWcvw2snUGOBTKfsCv1G1jE75FbsvEjtKeVkRqbeHWy4QzybVPIg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f8dce32a-5551-4a49-9794-fc2f66261ee6",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6b72ecf2-549e-4110-a4ab-62a720d9b904",
                            Email = "hieuhv@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Ho Van",
                            IsActive = false,
                            LastName = "Hieu",
                            LockoutEnabled = false,
                            NormalizedEmail = "HIEUHV@GMAIL.COM",
                            NormalizedUserName = "HIEUHV@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEO12J6gX030BOEN7VknYqvPCfQJHNr3/b7QBX8CdA6UxmCwL9BNOTzGSbkJOzlOtsQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fe312aea-4937-4f83-aaa6-760cea5b1570",
                            TwoFactorEnabled = false,
                            UserName = "hieuhv@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5c5f8e82-c3b1-4076-a9ca-beb6a315cc39",
                            Email = "vietlq@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Le Quang",
                            IsActive = false,
                            LastName = "Viet",
                            LockoutEnabled = false,
                            NormalizedEmail = "VIETLQ@GMAIL.COM",
                            NormalizedUserName = "VIETLQ@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAELDFNx3oyiAHXEtaRC7XFUPh3SEa02BSvsORcZC4AxbSAUqcYMsbfPgasNArnlMeYg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "71cd294d-e8af-437a-8895-7903d87a6e3f",
                            TwoFactorEnabled = false,
                            UserName = "vietlq@gmail.com"
                        });
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.Lottery", b =>
                {
                    b.Property<int>("LotteryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LotteryId"));

                    b.Property<string>("Company")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DrawDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("LotteryNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RewardId")
                        .HasColumnType("int");

                    b.HasKey("LotteryId");

                    b.HasIndex("RewardId");

                    b.ToTable("Lotteries");

                    b.HasData(
                        new
                        {
                            LotteryId = 1,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "123456",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 1
                        },
                        new
                        {
                            LotteryId = 2,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "234567",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 2
                        },
                        new
                        {
                            LotteryId = 3,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "345678",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 3
                        },
                        new
                        {
                            LotteryId = 4,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "456789",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 4
                        },
                        new
                        {
                            LotteryId = 5,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "567890",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 5
                        },
                        new
                        {
                            LotteryId = 6,
                            DrawDate = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsPublished = false,
                            LotteryNumber = "678901",
                            PublishDate = new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RewardId = 6
                        });
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.PurchaseTicket", b =>
                {
                    b.Property<int>("PurchaseTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseTicketId"));

                    b.Property<string>("LotteryNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PurchaseTicketId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseTickets");

                    b.HasData(
                        new
                        {
                            PurchaseTicketId = 1,
                            LotteryNumber = "123456",
                            PurchaseDate = new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("36b35306-154c-4518-8fc1-d7e756522111")
                        },
                        new
                        {
                            PurchaseTicketId = 2,
                            LotteryNumber = "234567",
                            PurchaseDate = new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d")
                        },
                        new
                        {
                            PurchaseTicketId = 3,
                            LotteryNumber = "345678",
                            PurchaseDate = new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d")
                        },
                        new
                        {
                            PurchaseTicketId = 4,
                            LotteryNumber = "456789",
                            PurchaseDate = new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("36b35306-154c-4518-8fc1-d7e756522111")
                        });
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("Jwtld")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.Reward", b =>
                {
                    b.Property<int>("RewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RewardId"));

                    b.Property<string>("RewardName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RewardValue")
                        .HasColumnType("int");

                    b.HasKey("RewardId");

                    b.ToTable("Rewards");

                    b.HasData(
                        new
                        {
                            RewardId = 1,
                            RewardName = "Prize 8",
                            RewardValue = 1000000
                        },
                        new
                        {
                            RewardId = 2,
                            RewardName = "Prize 7",
                            RewardValue = 2000000
                        },
                        new
                        {
                            RewardId = 3,
                            RewardName = "Prize 6",
                            RewardValue = 3000000
                        },
                        new
                        {
                            RewardId = 4,
                            RewardName = "Prize 5",
                            RewardValue = 4000000
                        },
                        new
                        {
                            RewardId = 5,
                            RewardName = "Prize 4",
                            RewardValue = 5000000
                        },
                        new
                        {
                            RewardId = 6,
                            RewardName = "Prize 3",
                            RewardValue = 6000000
                        },
                        new
                        {
                            RewardId = 7,
                            RewardName = "Prize 2",
                            RewardValue = 7000000
                        },
                        new
                        {
                            RewardId = 8,
                            RewardName = "Prize 1",
                            RewardValue = 8000000
                        });
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.SearchHistory", b =>
                {
                    b.Property<int>("SearchHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SearchHistoryId"));

                    b.Property<string>("LotteryNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SearchDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SearchHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("SearchHistories");

                    b.HasData(
                        new
                        {
                            SearchHistoryId = 1,
                            LotteryNumber = "123456",
                            SearchDate = new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("36b35306-154c-4518-8fc1-d7e756522111")
                        },
                        new
                        {
                            SearchHistoryId = 2,
                            LotteryNumber = "234567",
                            SearchDate = new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d")
                        },
                        new
                        {
                            SearchHistoryId = 3,
                            LotteryNumber = "345678",
                            SearchDate = new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"),
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("24dd0b58-c0e0-470c-8ed2-14467a3b868f"),
                            RoleId = new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca")
                        },
                        new
                        {
                            UserId = new Guid("36b35306-154c-4518-8fc1-d7e756522111"),
                            RoleId = new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e")
                        },
                        new
                        {
                            UserId = new Guid("57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d"),
                            RoleId = new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.Lottery", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.Reward", "Reward")
                        .WithMany("Lotteries")
                        .HasForeignKey("RewardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reward");
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.PurchaseTicket", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.RefreshToken", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.SearchHistory", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotteryChecker.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("LotteryChecker.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LotteryChecker.Core.Entities.Reward", b =>
                {
                    b.Navigation("Lotteries");
                });
#pragma warning restore 612, 618
        }
    }
}
