using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/purchase-ticket")]
    public class PurchaseTicketController : Controller
    {
        public PurchaseTicketController() { }

        [HttpGet]
        [Route("")]
        [Route("{page}/{pageSize}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Index(int page=1, int pageSize=5)
        {
            try
            {
                if (Request.Cookies["User"] != null)
                {
                    var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
                    ViewBag.UserEmail = user.Email;
                }

                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Get,
                                       $"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets?page={page}&pageSize={pageSize}", null,
                                                          Request.Cookies["AccessToken"]);
                if (response.Data != null)
                    return View(response.Data);
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View();
                }
            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [Route("get-purchase-ticket/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Get,
                                                  $"{Constants.API_PURCHASE_TICKET}/get-purchase-ticket/{id}", null, Request.Cookies["AccessToken"]);

                if (Request.Cookies["User"] != null)
                {
                    var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
                    ViewBag.UserEmail = user.Email;
                }

                if (response.Data?.Result != null)
                    return View(response.Data.Result.FirstOrDefault());
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View();
                }
            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("create-purchase-ticket")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                                                                     $"{Constants.API_USER}/get-all-users/page=1&pageSize=9999", accessToken: Request.Cookies["AccessToken"]);
                if (response.Errors != null)

                    return RedirectToAction("Index");
                else
                {
                    var userList = response.Data?.Result;
                    ViewBag.Users = new SelectList(userList, "UserName", "Id");

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
        [Route("create-purchase-ticket")]
        [CustomAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseTicketVm purchaseTicket)
        {
            try
            {
                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Post,
                                                                     $"{Constants.API_PURCHASE_TICKET}/create-purchase-ticket", purchaseTicket, Request.Cookies["AccessToken"]);
                if (response.Data != null)
                 
                return RedirectToAction("Index");
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View();
                }
            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("update-purchase-ticket/{ticketId}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Edit(int ticketId)
        {
            try
            {
                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Get,
                                                                     $"{Constants.API_PURCHASE_TICKET}/get-purchase-ticket/{ticketId}", null, Request.Cookies["AccessToken"]);

                if (response.Data?.Result == null)
                {
                    TempData["Errors"] = "Failed to load reward details.";
                    return RedirectToAction("Index");
                }

                return View(response.Data.Result.FirstOrDefault());


            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [Route("update-purchase-ticket/{id}")]
        [CustomAuthorize("Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseTicketVm purchaseTicket)
        {
            try
            {
                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Put,
                                                                                        $"{Constants.API_PURCHASE_TICKET}/update-purchase-ticket/{id}", purchaseTicket, Request.Cookies["AccessToken"]);
                if (response.Data?.Result != null)
                {
                    TempData["Messages"] = "Updated successfully!";
                    return RedirectToAction("Index");
                }
                    
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View();
                }
            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [Route("delete-purchase-ticket/{id}")]
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await HttpUtils<PurchaseTicketVm>.SendRequest(HttpMethod.Post,
                                                                                                      $"{Constants.API_PURCHASE_TICKET}/delete-purchase-ticket/{id}", null, Request.Cookies["AccessToken"]);
                if (response.Data != null)
                    return RedirectToAction("Index");
                else
                {
                    TempData["Errors"] = response.Errors;
                    return View("Index");
                }
            } catch (Exception ex)
            {
                TempData["Errors"] = ex.Message;
                return View("Index");
            }
        }




    }
}
