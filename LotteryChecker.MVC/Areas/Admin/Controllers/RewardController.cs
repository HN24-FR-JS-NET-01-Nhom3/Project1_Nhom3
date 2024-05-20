using Microsoft.AspNetCore.Mvc;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Newtonsoft.Json;
using LotteryChecker.Core.Entities;

namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/reward")]
    public class RewardController : Controller
    {
        public RewardController() { }

        [HttpGet]
        [Route("")]
        [Route("{page}/{pageSize}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {
                if (Request.Cookies["User"] != null)
                {
                    var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
                    TempData["UserId"] = user.Id;
                }

                var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Get,
                                       $"{Constants.API_REWARD}/get-all-rewards?page={page}&pageSize={pageSize}", null,
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
                if (response.Data?.Result != null)
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

        [HttpGet]
        [Route("create")]
        [CustomAuthorize("Admin")]
        public IActionResult Create()
        {
            return View();
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

        [HttpGet]
        [Route("edit-reward/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Get,
                               $"{Constants.API_REWARD}/get-reward/{id}", null, Request.Cookies["AccessToken"]);

            if (response.Data?.Result == null)
            {
                TempData["Errors"] = "Failed to load reward details.";
                return RedirectToAction("Index");
            }

            return View(response.Data.Result.FirstOrDefault());
        }

        [HttpPost]
        [Route("edit-reward/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Edit(int id, RewardVm rewardVm)
        {
          
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await HttpUtils<RewardVm>.SendRequest(HttpMethod.Put,
                                          $"{Constants.API_REWARD}/update-reward/{id}", rewardVm, Request.Cookies["AccessToken"]);
                    if (response.Errors == null)
                    {
                        TempData["Messages"] = "Updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Errors"] = response.Errors;
                        return View(rewardVm);
                    }
                   
                }
                return View(rewardVm);
            }
            catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }
    }
}
