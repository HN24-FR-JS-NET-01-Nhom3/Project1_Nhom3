using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[CustomAuthorize("Admin, Contributor")]
public class HomeAdminController : Controller
{
    [Route("admin")]
    public IActionResult Index()
    {
        return View();
    }
}