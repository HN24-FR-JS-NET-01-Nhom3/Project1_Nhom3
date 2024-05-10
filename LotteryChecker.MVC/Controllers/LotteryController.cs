using AutoMapper;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Models.Entities;
using LotteryChecker.MVC.Models.ViewModels;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
				CurrentDate = lotteryResult.IsNullOrEmpty()
					? year != null && month != null && day != null ? new DateTime((int)year!, (int)month!, (int)day!) : DateTime.Now
					: lotteryResult.First().DrawDate
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}

	[HttpGet]
	[Route("check-ticket")]
	[Route("check-ticket/{year}/{month}/{day}/{ticketNumber}")]
	public async Task<IActionResult> CheckTicket(string? ticketNumber, int? year, int? month, int? day)
	{
		try
		{
			if (ticketNumber != null && year != null && month != null && day != null)
			{
				var lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");					
				TempData["LotteryResult"] = (lotteryResponse ?? []).GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);
				
				var searchTicketVm = new SearchTicketVm()
				{
					TicketNumber = ticketNumber,
					DrawDate = new DateTime((int)year, (int)month, (int)day)
				};

				var searchResponse = await HttpUtils<RewardVm>.SendRequestAndProcessResponse(HttpMethod.Post,
					$"{Constants.API_LOTTERY}/get-ticket-result", JsonConvert.SerializeObject(searchTicketVm));
				if (searchResponse == null)
				{
					TempData["Reward"] = new RewardVm()
					{
						RewardValue = -1
					};
				}
				else
				{
					TempData["Reward"] = searchResponse;
				}

				return View(searchTicketVm);
			}

			return View();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}

	[HttpPost]
	[Route("check-ticket")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CheckTicket(SearchTicketVm? searchTicketVm)
	{
		if (ModelState.IsValid)
		{
			if (searchTicketVm == null)
			{
				return View();
			}

			try
			{
				var lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequestAndProcessResponse(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={searchTicketVm.DrawDate.Year}&month={searchTicketVm.DrawDate.Month}&day={searchTicketVm.DrawDate.Day}");					
				TempData["LotteryResult"] = (lotteryResponse ?? []).GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);

				var searchResponse = await HttpUtils<RewardVm>.SendRequestAndProcessResponse(HttpMethod.Post,
					$"{Constants.API_LOTTERY}/get-ticket-result", JsonConvert.SerializeObject(searchTicketVm));
				if (searchResponse == null)
				{
					TempData["Reward"] = new RewardVm()
					{
						RewardValue = -1
					};
				}
				else
				{
					TempData["Reward"] = searchResponse;
				}
				
				return View(searchTicketVm);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		return View();
	}
}