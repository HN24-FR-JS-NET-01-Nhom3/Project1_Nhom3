using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/reward")]
    public class RewardController : Controller
    {

        public RewardController() { }

        [HttpGet]
        [Route("get-all-rewards")]
        [Route("get-all-rewards/{page}/{pageSize}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            try
            {
                if (Request.Cookies["User"] != null)
                {
                    var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
                    TempData["UserId"] = user.Id;
                }

                var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Get,
                                       $"{Constants.API_REWARD}/get-all-rewards/", null,
                                                          Request.Cookies["AccessToken"]);

                if (response.Data != null)
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

        [Route("get-reward/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Get,
                               $"{Constants.API_REWARD}/get-reward/{id}", null, Request.Cookies["AccessToken"]);
                if (response.Data != null)
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

        [HttpPost]
        [Route("create")]
        [CustomAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RewardVm rewardVm)
        {
            try
            {
                var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
                                                  $"{Constants.API_REWARD}/create-reward", rewardVm, Request.Cookies["AccessToken"]);
                if (response.Data != null)
                    return RedirectToAction("Index");
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

        [HttpPost]
        [Route("edit-reward/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> EditReward(RewardVm rewardVm)
        {
            try
            {
                var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Post,
                                              $"{Constants.API_REWARD}/edit-reward/{rewardVm.RewardName}", rewardVm, Request.Cookies["AccessToken"]);
                if (response.Data != null)
                    return RedirectToAction("Index");
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
    }

   
}
