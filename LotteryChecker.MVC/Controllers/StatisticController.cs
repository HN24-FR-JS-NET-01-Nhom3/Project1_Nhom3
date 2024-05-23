using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Controllers;

[Route("statistic")]
public class StatisticController : BaseController
{
	public StatisticController(IMapper mapper) : base(mapper)
	{
	}

	[Route("")]
	[CustomAuthorize("User, Admin")]
	public async Task<IActionResult> Index()
	{
		var response = await HttpUtils<StatisticVm>.SendRequest(HttpMethod.Get,
			$"{Constants.API_STATISTIC}/get-statistic-for-user", accessToken: Request.Cookies["AccessToken"]);

		if (response.Errors == null)
		{
			return View(response.Data?.Result?.First());
		}

		TempData["Message"] = string.Join(',', response.Errors);
		return View();
	}

	[HttpPost]
	[Route("edit-user")]
	[CustomAuthorize("User, Admin")]
	public async Task<IActionResult> EditUser(UserVm userVm)
	{
		throw new NotImplementedException();
	}
    
	[HttpGet]
	[Route("change-password")]
	[CustomAuthorize("Admin, User")]
	public IActionResult ChangePassword()
	{
		return View();
	}

	[HttpPost]
	[Route("change-password")]
	[CustomAuthorize("Admin, User")]
	public async Task<IActionResult> ChangePassword(ChangePasswordVm changePasswordVm)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var response = await HttpUtils<string>.SendRequest(HttpMethod.Post,
					$"{Constants.API_AUTHEN}/change-password", changePasswordVm, Request.Cookies["AccessToken"]);
				if (response.Message != null)
				{
					TempData["Messages"] = "Change password successfully!";
					return RedirectToAction("Index", "Lottery");
				}

				if (response.Errors != null)
				{
					foreach (var error in response.Errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}

					return View();
				}
			}

			return View();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}