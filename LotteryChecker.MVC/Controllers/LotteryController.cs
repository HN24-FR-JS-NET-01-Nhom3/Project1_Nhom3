using AutoMapper;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Models.Entities;
using LotteryChecker.MVC.Models.ViewModels;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.MVC.Controllers;

[Route("lottery")]
public class LotteryController : Controller
{
	private readonly IMapper _mapper;
	public LotteryController(IMapper mapper)
	{
		_mapper = mapper;
	}

	[Route("")]
	[Route("{year}/{month}/{day}")]
	public async Task<IActionResult> Index(int? year, int? month, int? day)
	{
		try
		{
			IEnumerable<LotteryVm>? lotteryResponse;
			if (year != null && month != null && day != null)
			{
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
			var lotteryResult = lotteryResponse.ToList();

			var rewardResponse =
				await HttpUtils<HttpResponse<RewardVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
					$"{Constants.API_REWARD}/get-all-rewards");
			
			var rewardPool = rewardResponse?.Result.Reverse();

			return View(new LotteriesVm()
			{
				LotteryVmGroups = lotteryResult.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key),
				RewardVms = _mapper.Map<List<RewardVm>>(rewardPool),
				CurrentDate = lotteryResult.IsNullOrEmpty() ? new DateTime((int)year!, (int)month!, (int)day!) : lotteryResult.First().DrawDate
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}