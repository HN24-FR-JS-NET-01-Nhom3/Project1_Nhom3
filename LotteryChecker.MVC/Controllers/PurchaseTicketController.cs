using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LotteryChecker.Common.Models.ViewModels;

namespace LotteryChecker.MVC.Controllers;

[Route("purchase-ticket")]
public class PurchaseTicketController : BaseController
{
    public PurchaseTicketController(IMapper mapper) : base(mapper)
    {
    }

    [HttpGet("")]
    [CustomAuthorize("User, Admin")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var apiResponse = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Get, 
                $"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets", accessToken: Request.Cookies["AccessToken"]);

            if (apiResponse.Errors == null)
            {
                return View(apiResponse.Data?.Result?.ToList());
            }

            TempData["ErrorMessage"] = apiResponse.Errors;
            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
    }

    [CustomAuthorize("User, Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(PurchaseTicket purchaseTicket)
    {
        try
        {
            if (TempData["User"] != null)
            {
                var userData = TempData["User"].ToString();
                var user = JsonConvert.DeserializeObject<UserVm>(userData);
                if (user != null)
                {
                    purchaseTicket.UserId = user.Id;
                }
            }
            var currentDate = DateTime.Now;
            purchaseTicket.PurchaseDate = currentDate;
                
            var apiResponse = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Post,
                $"{Constants.API_PURCHASE_TICKET}/create-purchase-ticket", purchaseTicket, Request.Cookies["AccessToken"]);

            if (apiResponse.Errors == null)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = apiResponse.Errors;
            return View("Index");
        }
        catch (Exception ex)
        {

            TempData["ErrorMessage"] = ex.Message;
            return View("Index");
        }
    }
}
