using LotteryChecker.Common.Models.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/sarch-history")]
    public class SearchHistoryController : Controller
    {
        public SearchHistoryController()
        {
        }
        [Route("")]
        [Route("{page}/{pageSize}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {
                var response = await HttpUtils<SearchHistoryVm>.SendRequest(HttpMethod.Get,
                    $"{Constants.API_SEARCH_HISTORY}/get-all-search-histories?page={page}&pageSize={pageSize}", null,
                    Request.Cookies["AccessToken"]);

                if(response.Data != null)
                    return View(response.Data);
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [Route("get-search-history/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await HttpUtils<SearchHistoryVm>.SendRequest(HttpMethod.Get,
                    $"{Constants.API_SEARCH_HISTORY}/get-search-history/{id}", accessToken: Request.Cookies["AccessToken"]);
                if(response.Data?.Result != null)
                    return View(response.Data.Result.FirstOrDefault());
                else
                {

                    TempData["Errors"] = response.Errors;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
