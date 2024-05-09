using System.Security.Claims;
using AutoMapper;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LotteryChecker.MVC.Controllers;

public class BaseController : Controller
{
	protected readonly IMapper _mapper;
	private readonly UserManager<AppUser> _userManager;

	public BaseController(IMapper mapper, UserManager<AppUser> userManager)
	{
		_mapper = mapper;
		_userManager = userManager;
	}

	public override void OnActionExecuting(ActionExecutingContext context)
	{
		// var user = HttpContext.Session.GetString("User");
		// TempData["User"] = user;
		base.OnActionExecuting(context);
	}
}