using AutoMapper;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Models.ViewModels;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Controllers;

[Route("authen")]
public class AuthenController : BaseController
{
	public AuthenController(IMapper mapper, UserManager<AppUser> userManager) : base(mapper, userManager)
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
					$"{Constants.API_AUTHEN}/login", JsonConvert.SerializeObject(loginVm));
				if (loginResponse != null)
				{
					Response.Cookies.Append("AccessToken", loginResponse.AccessToken);
					Response.Cookies.Append("RefreshToken", loginResponse.RefreshToken);
					Response.Cookies.Append("User", JsonConvert.SerializeObject(loginResponse.User));
					
					return RedirectToAction("Index", "Lottery");
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