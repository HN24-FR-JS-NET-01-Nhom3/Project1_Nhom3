using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Common.Models.ViewModels;
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
			Response<LotteryVm> lotteryResponse;
			if (year != null && month != null && day != null)
			{
				lotteryResponse = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");
			}
			else
			{
				lotteryResponse =
					await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
						$"{Constants.API_LOTTERY}/get-lottery-result");
			}
			var lotteriesList = lotteryResponse.Data?.Result?.ToList();

			var rewardResponse =
				await HttpUtils<RewardVm>.SendRequest(HttpMethod.Get,
					$"{Constants.API_REWARD}/get-all-rewards");

			var rewardPool = rewardResponse.Data?.Result?.Reverse();

			return View(new ListLotteriesVm()
			{
				LotteryVmGroups = lotteriesList?.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key),
				RewardVms = _mapper.Map<List<RewardVm>>(rewardPool),
				CurrentDate = lotteriesList.IsNullOrEmpty()
					? year != null && month != null && day != null ? new DateTime((int)year, (int)month, (int)day) : DateTime.Now
					: lotteriesList?.First().DrawDate
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
	[Route("check-ticket/{year}/{month}/{day}/{lotteryNumber}")]
	public async Task<IActionResult> CheckTicket(string? lotteryNumber, int? year, int? month, int? day)
	{
		try
		{
			if (TempData["User"] != null)
			{
				var userData = TempData["User"].ToString();
				var user = JsonConvert.DeserializeObject<UserVm>(userData);
				if (user != null)
				{
					var searchHistoryResponse = await HttpUtils<SearchHistoryVm>.SendRequest(HttpMethod.Get,
						$"{Constants.API_SEARCH_HISTORY}/get-search-histories-by-user-id", accessToken: Request.Cookies["AccessToken"]);
					ViewData["SearchHistories"] = searchHistoryResponse.Data?.Result;
				}
			}
			
			if (lotteryNumber != null && year != null && month != null && day != null)
			{
				var lotteryResponse = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
					$"{Constants.API_LOTTERY}/get-lottery-result?year={year}&month={month}&day={day}");					
				ViewData["LotteryResult"] = lotteryResponse.Data?.Result?.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);
				
				var searchHistoryVm = new SearchHistoryVm()
				{
                    LotteryNumber = lotteryNumber,
					SearchDate = DateTime.Now,
					DrawDate = new DateTime((int)year, (int)month, (int)day)
				};
                var searchResponse = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_LOTTERY}/get-ticket-result", searchHistoryVm);
				if (searchResponse.Errors.IsNullOrEmpty())
				{
					ViewData["Reward"] = searchResponse.Data?.Result?.FirstOrDefault();
				}
				else
				{
					ViewData["ErrorMessage"] = searchResponse.Errors;
				}
				return View(searchHistoryVm);
			}

			return View();
		}
		catch (Exception ex)
		{
			ViewData["ErrorMessage"] = ex.Message;
		}

		return View();
	}

    [HttpPost]
    [Route("check-ticket")]
    [Route("check-ticket/{year}/{month}/{day}/{lotteryNumber}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckTicket(SearchHistoryVm? searchHistoryVm)
    {
        if (ModelState.IsValid)
        {
            if (searchHistoryVm == null)
            {
                return View();
            }
            try
            {
                var lotteryResponse = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
                    $"{Constants.API_LOTTERY}/get-lottery-result?year={searchHistoryVm.DrawDate.Year}&month={searchHistoryVm.DrawDate.Month}&day={searchHistoryVm.DrawDate.Day}");
                ViewData["LotteryResult"] = lotteryResponse.Data?.Result?.GroupBy(l => l.RewardId).OrderByDescending(g => g.Key);

                var searchResponse = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
                    $"{Constants.API_LOTTERY}/get-ticket-result", searchHistoryVm);
                if (searchResponse.Errors.IsNullOrEmpty())
                {
                    ViewData["Reward"] = searchResponse.Data?.Result?.FirstOrDefault();
                }
                else
                {
                    ViewData["ErrorMessage"] = searchResponse.Errors;
                }
                if (TempData["User"] != null)
                {
                    var userData = TempData["User"].ToString();
                    var user = JsonConvert.DeserializeObject<UserVm>(userData);
					if(user != null)
					{
                        var addSearchHistoryResponse = await HttpUtils<SearchHistoryVm>.SendRequest(HttpMethod.Post,
                       $"{Constants.API_SEARCH_HISTORY}/create-search-history", new SearchHistoryVm()
                       {
                           LotteryNumber = searchHistoryVm.LotteryNumber,
                           SearchDate = DateTime.Now,
                           UserId = user.Id,
						   DrawDate = searchHistoryVm.DrawDate,
						   Email = user.Email,
						   Prize = searchResponse.Data?.Result?.FirstOrDefault()?.RewardValue ?? 0
                       }, accessToken: Request.Cookies["AccessToken"]);
                    }
                }

                return RedirectToAction("CheckTicket", new
                {
                    year = searchHistoryVm.DrawDate.Year,
                    month = searchHistoryVm.DrawDate.Month,
                    day = searchHistoryVm.DrawDate.Day,
                    lotteryNumber = searchHistoryVm.LotteryNumber
                });
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        return View();
    }
}