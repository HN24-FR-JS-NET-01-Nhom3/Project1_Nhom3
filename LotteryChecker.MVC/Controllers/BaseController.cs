using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LotteryChecker.MVC.Controllers;

public class BaseController : Controller
{
	protected readonly IMapper _mapper;

	public BaseController(IMapper mapper)
	{
		_mapper = mapper;
	}

	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var user = Request.Cookies["User"];
		TempData["User"] = user;
		base.OnActionExecuting(context);
	}
}