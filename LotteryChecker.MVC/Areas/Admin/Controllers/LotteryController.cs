using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/lottery")]
    public class LotteryController : Controller
    {
        private readonly IMapper _mapper;

        public LotteryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all-lotteries")]
        [Route("get-all-lotteries/{page}/{pageSize}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 17)
        {
            try
            {
                if(Request.Cookies["User"] != null)
                {
                    var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
                    TempData["UserId"] = user.Id;
                }
                var response = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
                    $"{Constants.API_LOTTERY}/get-all-lotteries/page={page}&pageSize={pageSize}", null, 
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

        [Route("get-lottery/{id}")]
        [CustomAuthorize("Admin")]

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
                $"{Constants.API_LOTTERY}/get-lottery/{id}", null, Request.Cookies["AccessToken"]);
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
        [HttpPost]
        [Route("change-published-lottery/{id}/{isPublished}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> ChangePublishedLottery(int id, bool isPublished)
        {
            try
            {
                var response = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Post,
                    $"{Constants.API_LOTTERY}/update-pubished-lottery/{id}/{!isPublished}", null, Request.Cookies["AccessToken"]);
                if (response.Data != null)
                {
                    TempData["Messages"] = "Changed status successfully";
                    Console.WriteLine("ok");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = JsonConvert.DeserializeObject<ErrorVm>(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("edit-lottery/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Get,
                $"{Constants.API_LOTTERY}/get-lottery/{id}", null, Request.Cookies["AccessToken"]);

            if (response.Data?.Result == null)
            {
                TempData["Errors"] = "Failed to load lottery details.";
                return RedirectToAction("Index");
            }

            return View(response.Data.Result.FirstOrDefault());
        }

        [HttpPost]
        [Route("edit-lottery/{id}")]
        [CustomAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LotteryVm lotteryVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await HttpUtils<LotteryVm>.SendRequest(HttpMethod.Put,
                        $"{Constants.API_LOTTERY}/update-lottery/{id}", lotteryVm, Request.Cookies["AccessToken"]);                  

                    if (response.Errors == null)
                    {
                        TempData["Messages"] = "Updated successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Errors"] = response.Errors;
                        return View(lotteryVm);
                    }
                }
                return View(lotteryVm);
            }
            catch (Exception ex)
        {
                TempData["Errors"] = ex.Message;
                return View(lotteryVm);
            }
        }
    }
}
