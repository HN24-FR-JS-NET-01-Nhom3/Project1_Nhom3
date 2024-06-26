using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LotteryChecker.MVC.Models;

namespace LotteryChecker.MVC.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return RedirectToAction("Index", "Lottery");
	}

	public IActionResult Privacy()
	{
		return View();
	}
}