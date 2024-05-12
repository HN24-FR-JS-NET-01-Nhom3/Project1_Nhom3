using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Controllers;

[Route("lottery")]
public class LotteryController : BaseController
{
	public LotteryController(IMapper mapper) : base(mapper)
	{
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
				lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");
			}
			else
			{
				lotteryResponse =
					await HttpUtils<IEnumerable<LotteryVm>>.SendRequest(HttpMethod.Get,
						$"{Constants.API_LOTTERY}/get-lottery-result");
			}

			if (lotteryResponse == null) lotteryResponse = [];
			var lotteryResult = lotteryResponse.ToList();

			var rewardResponse =
				await HttpUtils<HttpResponse<RewardVm>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_REWARD}/get-all-rewards");

			var rewardPool = rewardResponse?.Result.Reverse();

			return View(new LotteriesVm()
			{
				LotteryVmGroups = lotteryResult.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key),
				RewardVms = _mapper.Map<List<RewardVm>>(rewardPool),
				CurrentDate = lotteryResult.IsNullOrEmpty()
					? year != null && month != null && day != null ? new DateTime((int)year, (int)month, (int)day) : DateTime.Now
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
				var lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");					
				ViewData["LotteryResult"] = (lotteryResponse ?? []).GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);
				
				var searchTicketVm = new SearchTicketVm()
				{
					TicketNumber = ticketNumber,
					DrawDate = new DateTime((int)year, (int)month, (int)day)
				};

				var searchResponse = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_LOTTERY}/get-ticket-result", searchTicketVm);
				ViewData["Reward"] = searchResponse;

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
	[Route("check-ticket/{year}/{month}/{day}/{ticketNumber}")]
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
				var lotteryResponse = await HttpUtils<IEnumerable<LotteryVm>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={searchTicketVm.DrawDate.Year}&month={searchTicketVm.DrawDate.Month}&day={searchTicketVm.DrawDate.Day}");					
				ViewData["LotteryResult"] = (lotteryResponse ?? []).GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);

				var searchResponse = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_LOTTERY}/get-ticket-result", searchTicketVm);
				ViewData["Reward"] = searchResponse;

				if (TempData["User"] is AppUser user)
				{
					var addSearchHistoryResponse = await HttpUtils<SearchHistory>.SendRequest(
						HttpMethod.Post,
						$"{Constants.API_SEARCH_HISTORY}/create-search-history", new SearchHistoryVm()
						{
							LotteryNumber = searchTicketVm.TicketNumber,
							SearchDate = DateTime.Now,
							UserId = user.Id
						});
				}
				
				return RedirectToAction("CheckTicket", new { 
					year = searchTicketVm.DrawDate.Year,
					month = searchTicketVm.DrawDate.Month,
					day = searchTicketVm.DrawDate.Day,
					ticketNumber = searchTicketVm.TicketNumber
				});
			}
			catch (Exception ex)
			{
				var response = JsonConvert.DeserializeObject<ErrorVm>(ex.Message);
				if (response.StatusCode == 400)
				{
					ViewData["ErrorMessage"] = response.Message;
				}
			}
		}

		return View();
	}
}