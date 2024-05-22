using Asp.Versioning;
using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LotteryChecker.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/statistic")]
public class StatisticsController : ControllerBase
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public StatisticsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_userManager = userManager;
	}


	[HttpGet("get-statistic-for-admin")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetAdminStatistics([FromQuery] int numberOfMonths = 4)
	{
		try
		{
			var result = new AdminStatisticVm()
			{
				UserCount = _userManager.Users.Count(),
				RewardCount = _unitOfWork.RewardRepository.GetAll().Count(),
				LotteryCount = _unitOfWork.LotteryRepository.GetAll().Count(),
				PurchaseCount = _unitOfWork.PurchaseTicketRepository.GetAll().Count(),
				MonthlyStatistic = GetMonthlyStatistics(numberOfMonths)
			};

			return Ok(new Response<AdminStatisticVm>()
			{
				Data = new Data<AdminStatisticVm>()
				{
					Result = new[] { result }
				}
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				Errors = new[] { ex.Message }
			});
		}
	}
	
	private List<AdminMonthlyStatisticVm> GetMonthlyStatistics(int numberOfMonths)
	{
		var currentDate = DateTime.Now;

		// Lấy dữ liệu thống kê cho từng tháng trong số tháng gần nhất
		var monthlyStatistics = new List<AdminMonthlyStatisticVm>();
		for (var i = 0; i < numberOfMonths; i++)
		{
			var monthDate = currentDate.AddMonths(-i);
			var lotteries = _unitOfWork.LotteryRepository.Find(l => l.PublishDate?.Month == monthDate.Month && l.PublishDate?.Year == monthDate.Year);
			var purchases = _unitOfWork.PurchaseTicketRepository.Find(pt => pt.PurchaseDate?.Month == monthDate.Month && pt.PurchaseDate?.Year == monthDate.Year);
			var searches = _unitOfWork.SearchHistoryRepository.Find(pt => pt.SearchDate.Month == monthDate.Month && pt.SearchDate.Year == monthDate.Year);


			monthlyStatistics.Add(new AdminMonthlyStatisticVm
			{
				Month = monthDate.Month,
				Year = monthDate.Year,
				LotteryCount = lotteries.Count(),
				PurchaseCount = purchases.Count(),
				SearchCount = searches.Count()
			});
		}

		monthlyStatistics = monthlyStatistics.OrderBy(ms => ms.Year).ThenBy(ms => ms.Month).ToList();
		return monthlyStatistics;
	}

	[HttpGet("get-statistic-for-user")]
	[Authorize(Roles = "User, Admin")]
	public async Task<IActionResult> GetUserStatistic([FromQuery] int numberOfMonths = 4)
	{
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (userId == null)
		{
			return NotFound(new Response<string>()
			{
				Errors = new[] { "User not found!" }
			});
		}

		var user = await _userManager.FindByIdAsync(userId);
		if (user == null)
		{
			return NotFound(new Response<string>()
			{
				Errors = new[] { "User not found!" }
			});
		}

		var currentDate = DateTime.Now;
		var purchaseTickets = _unitOfWork.PurchaseTicketRepository
			.Find(pt => pt.UserId.ToString() == userId)
			.ToList();

		var monthlyStatistics = new List<MonthlyStatisticVm>();

		for (var i = 0; i < numberOfMonths; i++)
		{
			var monthDate = currentDate.AddMonths(-i);
			var purchases = purchaseTickets
				.Where(pt => pt.PurchaseDate?.Month == monthDate.Month && pt.PurchaseDate?.Year == monthDate.Year)
				.ToList();

			var wins = purchases.Count(pt =>
				GetTicketResult(pt) != 0);
			var moneyWin = purchases
				.Where(pt => GetTicketResult(pt) != 0)
				.Sum(GetTicketResult);

			monthlyStatistics.Add(new MonthlyStatisticVm
			{
				Month = monthDate.Month,
				Year = monthDate.Year,
				PurchaseCount = purchases.Count,
				WinCount = wins,
				MoneyWinCount = moneyWin
			});
		}

		monthlyStatistics = monthlyStatistics.OrderBy(ms => ms.Year).ThenBy(ms => ms.Month).ToList();

		var thisMonthPurchases = purchaseTickets
			.Where(pt => pt.PurchaseDate?.Month == currentDate.Month && pt.PurchaseDate?.Year == currentDate.Year)
			.ToList();

		var lastMonthDate = currentDate.AddMonths(-1);
		var lastMonthPurchases = purchaseTickets
			.Where(pt => pt.PurchaseDate?.Month == lastMonthDate.Month && pt.PurchaseDate?.Year == lastMonthDate.Year)
			.ToList();

		var thisMonthValue = thisMonthPurchases.Count;
		var lastMonthValue = lastMonthPurchases.Count;

		var thisMonthWins = thisMonthPurchases.Count(pt => GetTicketResult(pt) != 0);
		var lastMonthWins = lastMonthPurchases.Count(pt => GetTicketResult(pt) != 0);

		var thisMonthMoney = thisMonthPurchases
			.Where(pt => GetTicketResult(pt) != 0)
			.Sum(pt => GetTicketResult(pt));

		var lastMonthMoney = lastMonthPurchases
			.Where(pt => GetTicketResult(pt) != 0)
			.Sum(GetTicketResult);

		var response = new StatisticVm
		{
			User = _mapper.Map<UserVm>(user),
			PurchaseCount = new CardInfoVm()
			{
				Title = "Số vé đã mua",
				Value = thisMonthValue,
				DifferentValueWithLastMonth = thisMonthValue - lastMonthValue,
				DifferentPercentWithlastMonth =
					lastMonthValue != 0 ? (thisMonthValue / (double)lastMonthValue) * 100 : 0
			},
			WinCount = new CardInfoVm()
			{
				Title = "Số lần trúng thưởng",
				Value = thisMonthWins,
				DifferentValueWithLastMonth = thisMonthWins - lastMonthWins,
				DifferentPercentWithlastMonth = lastMonthWins != 0 ? (thisMonthWins / (double)lastMonthWins) * 100 : 0
			},
			MoneyWinCount = new CardInfoVm()
			{
				Title = "Số tiền trúng thưởng",
				Value = thisMonthMoney,
				DifferentValueWithLastMonth = thisMonthMoney - lastMonthMoney,
				DifferentPercentWithlastMonth =
					lastMonthMoney != 0 ? (thisMonthMoney / (double)lastMonthMoney) * 100 : 0
			},
			MonthlyStatistics = monthlyStatistics
		};

		return Ok(new Response<StatisticVm>()
		{
			Data = new Data<StatisticVm>()
			{
				Result = new[] { response }
			}
		});
	}

	private int GetTicketResult(PurchaseTicket purchaseTicket)
	{
		var lotteries = _unitOfWork.LotteryRepository.GetLotteryResult(purchaseTicket.DrawDate).ToList();
		lotteries = lotteries.OrderBy(l => l.RewardId).ToList();

		foreach (var lottery in lotteries)
		{
			if (purchaseTicket.LotteryNumber.EndsWith(lottery.LotteryNumber))
			{
				var reward = _unitOfWork.RewardRepository.GetById(lottery.RewardId);
				return _mapper.Map<RewardVm>(reward).RewardValue;
			}
		}

		try
		{
			var specialPriceLottery = lotteries.First(l => l.RewardId == 1);
			if (purchaseTicket.LotteryNumber.EndsWith(specialPriceLottery.LotteryNumber.Substring(1)))
			{
				var reward = _unitOfWork.RewardRepository.GetById(9);
				return _mapper.Map<RewardVm>(reward).RewardValue;
			}

			var countDuplicate = specialPriceLottery.LotteryNumber.Where((t, i) => purchaseTicket.LotteryNumber[i] == t).Count();
			if (countDuplicate == 5)
			{
				var reward = _unitOfWork.RewardRepository.GetById(10);
				return _mapper.Map<RewardVm>(reward).RewardValue;
			}

			return 0;
		}
		catch (Exception ex)
		{
			return 0;
		}
	}
}