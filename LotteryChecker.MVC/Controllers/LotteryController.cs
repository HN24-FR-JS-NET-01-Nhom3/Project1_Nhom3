using AutoMapper;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Models.Entities;
using LotteryChecker.MVC.Models.ViewModels;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Controllers;

public class LotteryController : Controller
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public LotteryController(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	[Route("Lottery")]
	[Route("Lottery/{year}/{month}/{day}")]
	public async Task<IActionResult> Index(int? year, int? month, int? day)
	{
		try
		{
			var dateTime = DateTime.Now;
			IEnumerable<LotteryVm>? lotteryResponse;
			if (year != null && month != null && day != null)
			{
				dateTime = new DateTime((int)year, (int)month, (int)day);
				lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");
			}
			else
			{
				lotteryResponse =
					await HttpUtils<IEnumerable<LotteryVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
						$"{Constants.API_LOTTERY}/get-lottery-result");
			}

			if (lotteryResponse == null) lotteryResponse = [];

			var rewardResponse =
				await HttpUtils<HttpResponse<RewardVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
					$"{Constants.API_REWARD}/get-all-rewards");
			
			var rewardPool = rewardResponse?.Result.Reverse();

			return View(new LotteriesVm()
			{
				LotteryVmGroups = lotteryResponse.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key),
				RewardVms = _mapper.Map<List<RewardVm>>(rewardPool),
				CurrentDate = dateTime
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}