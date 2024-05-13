using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Controllers;

[Route("authen")]
public class AuthenController : BaseController
{
	public AuthenController(IMapper mapper) : base(mapper)
	{
	}

	[HttpGet]
	[Route("login")]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login(LoginVm loginVm)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var loginResponse = await HttpUtils<AuthResultVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_AUTHEN}/login", loginVm);
				if (loginResponse?.Data != null)
				{
					Response.Cookies.Append("AccessToken", loginResponse?.Data?.Result?.First().AccessToken);
					Response.Cookies.Append("RefreshToken", loginResponse?.Data?.Result?.First().RefreshToken);
					Response.Cookies.Append("User",
						JsonConvert.SerializeObject(loginResponse.Data?.Result?.First().User));
					return RedirectToAction("Index", "Lottery");
				}
				return View();
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