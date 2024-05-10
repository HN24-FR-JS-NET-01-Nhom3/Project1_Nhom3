using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
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
	public async Task<IActionResult> Login(LoginVm loginVm, string returnUrl)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var loginResponse = await HttpUtils<AuthResultVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_AUTHEN}/login", loginVm);
				if (loginResponse != null)
				{
					Response.Cookies.Append("AccessToken", loginResponse.AccessToken);
					Response.Cookies.Append("RefreshToken", loginResponse.RefreshToken);
					Response.Cookies.Append("User", JsonConvert.SerializeObject(loginResponse.User));

					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						// Chuyển hướng đến returnUrl nếu nó là một đường dẫn hợp lệ trong ứng dụng
						return Redirect(returnUrl);
					}
                
					// Nếu không có returnUrl hoặc returnUrl không hợp lệ, chuyển hướng đến trang Index của LotteryController
					return RedirectToAction("Index", "Lottery", new { area = "" });
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