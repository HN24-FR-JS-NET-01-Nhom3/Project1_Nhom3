USE [FSA_Lottery]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/7/2024 10:13:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastLogin] [datetime2](7) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lotteries]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lotteries](
	[LotteryId] [int] IDENTITY(1,1) NOT NULL,
	[DrawDate] [datetime2](7) NOT NULL,
	[PublishDate] [datetime2](7) NULL,
	[LotteryNumber] [nvarchar](max) NOT NULL,
	[RewardId] [int] NOT NULL,
	[Company] [nvarchar](50) NULL,
	[IsPublished] [bit] NOT NULL,
 CONSTRAINT [PK_Lotteries] PRIMARY KEY CLUSTERED 
(
	[LotteryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseTickets]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseTickets](
	[PurchaseTicketId] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseDate] [datetime2](7) NOT NULL,
	[LotteryNumber] [nvarchar](max) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PurchaseTickets] PRIMARY KEY CLUSTERED 
(
	[PurchaseTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[Jwtld] [nvarchar](max) NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[AddDate] [datetime2](7) NOT NULL,
	[ExpireDate] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rewards]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rewards](
	[RewardId] [int] IDENTITY(1,1) NOT NULL,
	[RewardValue] [int] NOT NULL,
	[RewardName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Rewards] PRIMARY KEY CLUSTERED 
(
	[RewardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchHistories]    Script Date: 5/7/2024 10:13:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchHistories](
	[SearchHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[LotteryNumber] [nvarchar](max) NOT NULL,
	[SearchDate] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SearchHistories] PRIMARY KEY CLUSTERED 
(
	[SearchHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240504080653_InitialDb', N'8.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240505094756_HieuHC2_UpdateDatabase', N'8.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240505111012_AddTableRefreshTokens', N'8.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240506155009_UpdateDatabaseLastTimeISwear', N'8.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240506160405_LastUpdate', N'8.0.0')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'fe0e9c2d-6abd-4f73-a635-63fc58ec700e', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b8fd818f-63f1-49ee-bec5-f7b66cafbfca', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d', N'fe0e9c2d-6abd-4f73-a635-63fc58ec700e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'36b35306-154c-4518-8fc1-d7e756522111', N'fe0e9c2d-6abd-4f73-a635-63fc58ec700e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'24dd0b58-c0e0-470c-8ed2-14467a3b868f', N'b8fd818f-63f1-49ee-bec5-f7b66cafbfca')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsActive], [LastLogin]) VALUES (N'24dd0b58-c0e0-470c-8ed2-14467a3b868f', N'Hoang Chi', N'Hieu', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJOd2Zf+BZMZrUivP8cjmOlkc1FVhrQ7s1Er97Bo5DTnBHc4G9aVqKtdKmS9WZxIuw==', N'2ce3adfd-9005-4bfe-93dc-841d3a2be324', N'1ed73add-d83b-4809-bc9c-7227b27bba6f', NULL, 0, 0, NULL, 0, 0, 1, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsActive], [LastLogin]) VALUES (N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d', N'Ho Van', N'Hieu', N'hieuhv@gmail.com', N'HIEUHV@GMAIL.COM', N'hieuhv@gmail.com', N'HIEUHV@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEIuGPp1rJPPEyl7NrZ1uL5cuYz0JL/9OrYGq3QwssJFM+eCunOJPb9AOLtloulKSkg==', N'40f45e9c-62eb-4b8c-9c35-f82ab38c26fa', N'228f62aa-f6ff-4be3-828b-1021a4c8d361', NULL, 0, 0, NULL, 0, 0, 0, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsActive], [LastLogin]) VALUES (N'36b35306-154c-4518-8fc1-d7e756522111', N'Le Quang', N'Viet', N'vietlq@gmail.com', N'VIETLQ@GMAIL.COM', N'vietlq@gmail.com', N'VIETLQ@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEDAQSs7+EzXTaFBQ7rkdlxizA2NvA5yQWmeudozViD0QUNCWwRrdV4xb2hhecw8s3A==', N'0725ada1-99c1-421b-9f64-3f11d630cd90', N'4d03f743-e76b-400b-bfcc-71177296eb1a', NULL, 0, 0, NULL, 0, 0, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Lotteries] ON 

INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (7, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'80', 8, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (8, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'005', 7, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (9, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'0383', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (10, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'4480', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (11, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'5656', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (12, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'1777', 5, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (13, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'24819', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (14, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'18552', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (15, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'85321', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (16, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'25409', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (17, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'16059', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (18, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'56837', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (19, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'67842', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (20, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'33956', 3, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (21, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'43359', 3, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (22, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'12875', 2, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (23, CAST(N'2024-05-06T10:00:11.6176367' AS DateTime2), CAST(N'2024-05-03T10:00:11.6176367' AS DateTime2), N'039807', 1, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (24, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'25', 8, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (25, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'390', 7, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (26, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'8798', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (27, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'0195', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (28, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'0921', 6, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (29, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'2248', 5, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (30, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'01608', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (31, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'86980', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (32, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'95606', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (33, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'41202', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (34, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'17427', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (35, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'59099', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (36, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'82502', 4, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (37, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'92139', 3, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (38, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'04057', 3, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (39, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'96582', 2, NULL, 1)
INSERT [dbo].[Lotteries] ([LotteryId], [DrawDate], [PublishDate], [LotteryNumber], [RewardId], [Company], [IsPublished]) VALUES (40, CAST(N'2024-05-07T16:40:17.1124377' AS DateTime2), CAST(N'2024-05-05T16:40:17.1124377' AS DateTime2), N'172043', 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[Lotteries] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseTickets] ON 

INSERT [dbo].[PurchaseTickets] ([PurchaseTicketId], [PurchaseDate], [LotteryNumber], [UserId]) VALUES (1, CAST(N'2024-05-10T00:00:00.0000000' AS DateTime2), N'123456', N'36b35306-154c-4518-8fc1-d7e756522111')
INSERT [dbo].[PurchaseTickets] ([PurchaseTicketId], [PurchaseDate], [LotteryNumber], [UserId]) VALUES (2, CAST(N'2024-05-11T00:00:00.0000000' AS DateTime2), N'234567', N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d')
INSERT [dbo].[PurchaseTickets] ([PurchaseTicketId], [PurchaseDate], [LotteryNumber], [UserId]) VALUES (3, CAST(N'2024-05-12T00:00:00.0000000' AS DateTime2), N'345678', N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d')
INSERT [dbo].[PurchaseTickets] ([PurchaseTicketId], [PurchaseDate], [LotteryNumber], [UserId]) VALUES (4, CAST(N'2024-05-13T00:00:00.0000000' AS DateTime2), N'456789', N'36b35306-154c-4518-8fc1-d7e756522111')
SET IDENTITY_INSERT [dbo].[PurchaseTickets] OFF
GO
SET IDENTITY_INSERT [dbo].[Rewards] ON 

INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (1, 2000000000, N'ĐB')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (2, 15000000, N'G.1')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (3, 10000000, N'G.2')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (4, 3000000, N'G.3')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (5, 1000000, N'G.4')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (6, 400000, N'G.5')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (7, 200000, N'G.6')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (8, 100000, N'G.7')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (9, 50000000, N'Giải An Ủi')
INSERT [dbo].[Rewards] ([RewardId], [RewardValue], [RewardName]) VALUES (10, 6000000, N'Giải Khúc Khích')
SET IDENTITY_INSERT [dbo].[Rewards] OFF
GO
SET IDENTITY_INSERT [dbo].[SearchHistories] ON 

INSERT [dbo].[SearchHistories] ([SearchHistoryId], [LotteryNumber], [SearchDate], [UserId]) VALUES (1, N'123456', CAST(N'2024-05-10T00:00:00.0000000' AS DateTime2), N'36b35306-154c-4518-8fc1-d7e756522111')
INSERT [dbo].[SearchHistories] ([SearchHistoryId], [LotteryNumber], [SearchDate], [UserId]) VALUES (2, N'234567', CAST(N'2024-05-11T00:00:00.0000000' AS DateTime2), N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d')
INSERT [dbo].[SearchHistories] ([SearchHistoryId], [LotteryNumber], [SearchDate], [UserId]) VALUES (3, N'345678', CAST(N'2024-05-12T00:00:00.0000000' AS DateTime2), N'57fa9a8e-3105-49a0-b0f2-6d88fdfcff8d')
SET IDENTITY_INSERT [dbo].[SearchHistories] OFF
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Lotteries] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsPublished]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Lotteries]  WITH CHECK ADD  CONSTRAINT [FK_Lotteries_Rewards_RewardId] FOREIGN KEY([RewardId])
REFERENCES [dbo].[Rewards] ([RewardId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lotteries] CHECK CONSTRAINT [FK_Lotteries_Rewards_RewardId]
GO
ALTER TABLE [dbo].[PurchaseTickets]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseTickets_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseTickets] CHECK CONSTRAINT [FK_PurchaseTickets_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[SearchHistories]  WITH CHECK ADD  CONSTRAINT [FK_SearchHistories_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchHistories] CHECK CONSTRAINT [FK_SearchHistories_AspNetUsers_UserId]
GO
