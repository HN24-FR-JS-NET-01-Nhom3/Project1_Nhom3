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
					HttpContext.Session.SetString("AccessToken", loginResponse.AccessToken);
					HttpContext.Session.SetString("RefreshToken", loginResponse.RefreshToken);
					HttpContext.Session.SetString("User", JsonConvert.SerializeObject(loginResponse.User));
					
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