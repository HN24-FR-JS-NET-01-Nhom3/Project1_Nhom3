using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Controllers;

// [Authorize]
public class LotteryController : Controller
{
	protected readonly IUnitOfWork _unitOfWork;
	public LotteryController(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public IActionResult Index()
	{
		return View();
	}
}