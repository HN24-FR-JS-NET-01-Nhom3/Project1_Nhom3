using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[CustomAuthorize("Admin, Contributor")]
public class HomeAdminController : Controller
{
    [Route("admin")]
    public async Task<IActionResult> Index()
    {
        var response = await HttpUtils<AdminStatisticVm>.SendRequest(HttpMethod.Get,
            $"{Constants.API_STATISTIC}/get-statistic-for-admin", accessToken: Request.Cookies["AccessToken"]);
        if (response.Errors == null)
        {
            return View(response.Data?.Result?.First());
        }
        return View();
    }
}