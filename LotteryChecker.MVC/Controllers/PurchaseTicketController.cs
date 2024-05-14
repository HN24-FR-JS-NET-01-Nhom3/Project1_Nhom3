using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using LotteryChecker.Common.Models.ViewModels;

namespace LotteryChecker.MVC.Controllers
{
    [Route("purchase-ticket")]
    public class PurchaseTicketController : BaseController
    {
        private readonly HttpClient _httpClient;

        public PurchaseTicketController(IMapper mapper) : base(mapper)
        {
            _httpClient = new HttpClient();
        }

        [Route("")]
        public async Task<IActionResult> Index([FromQuery] int? page = 1, [FromQuery] int? pageSize = 5)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets?page={page}&pageSize={pageSize}")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AccessToken"]);
                var apiResponse = await _httpClient.SendAsync(request);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseContent = await apiResponse.Content.ReadAsStringAsync();

                    var responseObject = JObject.Parse(responseContent);
                    var data = responseObject["data"];

                    if (data != null)
                    {
                        var purchaseTickets = data["result"].ToObject<List<PurchaseTicketVm>>();
                        return View(purchaseTickets);
                    }
                }
                else if (apiResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    var errorMessage = await apiResponse.Content.ReadAsStringAsync();
                    return StatusCode((int)apiResponse.StatusCode, errorMessage);
                }

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] PurchaseTicket purchaseTicket)
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
                purchaseTicket.DrawDate = currentDate;

                var purchaseTicketJson = JsonConvert.SerializeObject(purchaseTicket);
                var content = new StringContent(purchaseTicketJson, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{Constants.API_PURCHASE_TICKET}/create-purchase-ticket"),
                    Content = content
                };

                var accessToken = Request.Cookies["AccessToken"];
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var apiResponse = await _httpClient.SendAsync(request);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await apiResponse.Content.ReadAsStringAsync();
                    return StatusCode((int)apiResponse.StatusCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}