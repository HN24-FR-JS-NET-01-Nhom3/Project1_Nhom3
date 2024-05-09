using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Repositories;

public class LotteryRepository : BaseRepository<Lottery>, ILotteryRepository
{
	public LotteryRepository(LotteryContext context) : base(context)
	{
	}

	public IEnumerable<Lottery> GenerateLotteryResult(DateTime dateTime)
	{
		var lotteryList = new List<Lottery>();

		/*
		 * Reward 8: 1 slot
		 */
		lotteryList.Add(new Lottery()
		{
			DrawDate = dateTime,
			PublishDate = dateTime.AddDays(-3),
			LotteryNumber = new Random().Next((int)Math.Pow(10, 2)).ToString().PadLeft(2, '0'),
			IsPublished = true,
			RewardId = 8
		});

		/*
		 * Reward 7: 1 slot
		 */
		lotteryList.Add(new Lottery()
		{
			DrawDate = dateTime,
			PublishDate = dateTime.AddDays(-3),
			LotteryNumber = new Random().Next((int)Math.Pow(10, 3)).ToString().PadLeft(3, '0'),
			IsPublished = true,
			RewardId = 7
		});

		/*
		 * Reward 6: 3 slot
		 */
		for (int i = 0; i < 3; i++)
		{
			lotteryList.Add(new Lottery()
			{
				DrawDate = dateTime,
				PublishDate = dateTime.AddDays(-3),
				LotteryNumber = new Random().Next((int)Math.Pow(10, 4)).ToString().PadLeft(4, '0'),
				IsPublished = true,
				RewardId = 6
			});
		}

		/*
		 * Reward 5: 1 slot
		 */
		lotteryList.Add(new Lottery()
		{
			DrawDate = dateTime,
			PublishDate = dateTime.AddDays(-3),
			LotteryNumber = new Random().Next((int)Math.Pow(10, 4)).ToString().PadLeft(4, '0'),
			IsPublished = true,
			RewardId = 5
		});

		/*
		 * Reward 4: 7 slot
		 */
		for (int i = 0; i < 7; i++)
		{
			lotteryList.Add(new Lottery()
			{
				DrawDate = dateTime,
				PublishDate = dateTime.AddDays(-3),
				LotteryNumber = new Random().Next((int)Math.Pow(10, 5)).ToString().PadLeft(5, '0'),
				IsPublished = true,
				RewardId = 4
			});
		}

		/*
		 * Reward 3: 2 slot
		 */
		for (int i = 0; i < 2; i++)
		{
			lotteryList.Add(new Lottery()
			{
				DrawDate = dateTime,
				PublishDate = dateTime.AddDays(-3),
				LotteryNumber = new Random().Next((int)Math.Pow(10, 5)).ToString().PadLeft(5, '0'),
				IsPublished = true,
				RewardId = 3
			});
		}

		/*
		 * Reward 2: 1 slot
		 */
		lotteryList.Add(new Lottery()
		{
			DrawDate = dateTime,
			PublishDate = dateTime.AddDays(-3),
			LotteryNumber = new Random().Next((int)Math.Pow(10, 5)).ToString().PadLeft(5, '0'),
			IsPublished = true,
			RewardId = 2
		});

		/*
		 * Reward special: 1 slot
		 */
		lotteryList.Add(new Lottery()
		{
			DrawDate = dateTime,
			PublishDate = dateTime.AddDays(-3),
			LotteryNumber = new Random().Next((int)Math.Pow(10, 6)).ToString().PadLeft(6, '0'),
			IsPublished = true,
			RewardId = 1
		});

		CreateRange(lotteryList);
		return lotteryList.AsEnumerable();
	}

	public IEnumerable<Lottery> GetLotteryResult(DateTime dateTime)
	{
		return Find(x => x.DrawDate.Day == dateTime.Day
		                 && x.DrawDate.Month == dateTime.Month
		                 && x.DrawDate.Year == dateTime.Year);
	}

	public void UnpublishExpiredLotteries()
	{
		DbSet.Where(l => l.DrawDate.AddDays(-30) > DateTime.Now).ForEachAsync(l => l.IsPublished = false);
	}
}