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
	[Route("register")]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	[Route("register")]
	public async Task<IActionResult> Register(RegisterVm registerVm)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var registerResponse = await HttpUtils<AuthResultVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_AUTHEN}/register", registerVm);
				if (registerResponse.Message != null)
				{
					return View("Login");
				}

				if (registerResponse.Errors != null)
				{
					foreach (var error in registerResponse.Errors)
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
				if (loginResponse.Data != null)
				{
					Response.Cookies.Append("AccessToken", loginResponse?.Data?.Result?.First().AccessToken);
					Response.Cookies.Append("RefreshToken", loginResponse?.Data?.Result?.First().RefreshToken);
					Response.Cookies.Append("User",
						JsonConvert.SerializeObject(loginResponse.Data?.Result?.First().User));
					return RedirectToAction("Index", "Lottery");
				}

				if (loginResponse?.Errors != null)
				{
					foreach (var loginResponseError in loginResponse?.Errors)
					{
						ModelState.AddModelError(string.Empty, loginResponseError);
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

	[HttpGet]
	[Route("logout")]
	public IActionResult Logout()
	{
		Response.Cookies.Delete("AccessToken");
		Response.Cookies.Delete("RefreshToken");
		Response.Cookies.Delete("User");
		return RedirectToAction("Index", "Lottery");
	}

	[HttpGet]
	[Route("change-password")]
	public IActionResult ChangePassword()
	{
		throw new NotImplementedException();
	}

	[HttpGet("login-facebook")]
	public IActionResult LoginFacebook()
	{
		var redirectUrl = $"{Constants.API_AUTHEN}/login-facebook";
		return Redirect(redirectUrl);
	}

	[HttpGet("facebook-response")]
	public IActionResult FacebookResponse(string accessToken, string refreshToken, string user)
	{
		if (!string.IsNullOrEmpty(accessToken))
		{
			// Save token to cookie
			Response.Cookies.Append("AccessToken", accessToken);
			Response.Cookies.Append("RefreshToken", refreshToken);
			Response.Cookies.Append("User", user);

			// Redirect to home page or other page after login
			return RedirectToAction("Index", "Lottery");
		}

		// If token is null or empty, redirect to login page or show an error message
		ModelState.AddModelError(string.Empty, "Invalid token received from API.");
		return RedirectToAction("Index", "Home");
	}
	
	[HttpGet("login-google")]
	public IActionResult LoginGoogle()
	{
		var redirectUrl = $"{Constants.API_AUTHEN}/login-google";
		return Redirect(redirectUrl);
	}

	[HttpGet("google-response")]
	public async Task<IActionResult> GoogleResponse()
	{
		try
		{
			var tokenResponse = await HttpUtils<AuthResultVm>.SendRequest(HttpMethod.Get, $"{Constants.API_AUTHEN}/google-response");

			if (tokenResponse.Data != null)
			{
				// Save token to cookie
				Response.Cookies.Append("AccessToken", tokenResponse.Data?.Result?.First().AccessToken);

				// Redirect to home page or other page after login
				return RedirectToAction("Index", "Lottery");
			}

			// If there are errors, display error messages
			if (tokenResponse.Errors != null)
			{
				foreach (var error in tokenResponse.Errors)
				{
					ModelState.AddModelError(string.Empty, error);
				}
			}

			// Redirect to home page or other page after login
			return RedirectToAction("Index", "Lottery");
		}
		catch (Exception ex)
		{
			ModelState.AddModelError(string.Empty, ex.Message);
			return RedirectToAction("Index", "Lottery");
		}
	}

	[HttpGet("forgot-password")]
	public IActionResult ForgotPassword()
	{
		return View();
	}

	[HttpPost("forgot-password")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
	{
		if (ModelState.IsValid)
		{
			var response = await HttpUtils<string>.SendRequest(HttpMethod.Post,
				$"{Constants.API_AUTHEN}/forgot-password", forgotPasswordVm);
			if (response.Errors != null)
			{
				foreach (var error in response.Errors)
				{
					ModelState.AddModelError(string.Empty, error);
				}

				return View();
			}

			return RedirectToAction("ForgotPasswordConfirmation");
		}

		return View();
	}

	[HttpGet("forgot-password-confirm")]
	public IActionResult ForgotPasswordConfirmation()
	{
		return View();
	}
	
	[HttpGet("reset-password")]
	public IActionResult ResetPassword(string token, string email)
	{
		var model = new ResetPasswordVm() { Token = token, Email = email };
		return View(model);
	}

	[HttpPost("reset-password")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ResetPassword(ResetPasswordVm resetPasswordVm)
	{
		if (ModelState.IsValid)
		{
			var response = await HttpUtils<string>.SendRequest(HttpMethod.Post,
				$"{Constants.API_AUTHEN}/reset-password", resetPasswordVm);
			if (response.Errors != null)
			{
				foreach (var error in response.Errors)
				{
					ModelState.AddModelError(string.Empty, error);
				}

				return View();
			}

			return RedirectToAction("ResetPasswordConfirmation");
		}

		return View();
	}

	[HttpGet("reset-password-confirm")]
	public IActionResult ResetPasswordConfirmation()
	{
		return View();
	}
}