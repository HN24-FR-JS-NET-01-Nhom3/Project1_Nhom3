using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeAdminController : Controller
{
    [Route("admin")]
    public IActionResult Index()
    {
        return View();
    }
}