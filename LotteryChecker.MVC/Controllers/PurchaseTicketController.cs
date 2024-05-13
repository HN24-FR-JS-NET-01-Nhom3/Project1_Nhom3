using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace LotteryChecker.MVC.Controllers
{
    [Route("purchase-ticket")]
    public class PurchaseTicketController : Controller
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public PurchaseTicketController(IMapper mapper)
        {
            _httpClient = new HttpClient();
            _mapper = mapper;
        }

        [Route("")]

        public async Task<IActionResult> Index()
        {
            try
            {
                var purchaseTicketResponse = await HttpUtils<Response<PurchaseTicketVm>>.SendRequest(HttpMethod.Get, $"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets");
                ViewData["PurchaseTickets"] = purchaseTicketResponse;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://localhost:7178/api/v1/purchase-ticket/get-all-purchase-tickets")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIyNGRkMGI1OC1jMGUwLTQ3MGMtOGVkMi0xNDQ2N2EzYjg2OGYiLCJlbWFpbCI6ImFkbWluQGdtYWlsLmNvbSIsInN1YiI6ImFkbWluQGdtYWlsLmNvbSIsImp0aSI6Ijk1ZGVlYWViLTQ4YzAtNDRhYy1iZjE2LTE1NjdlMzhiM2EwMyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE1NjIwNTAxLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTc4IiwiYXVkIjoiVXNlciJ9.IVIy4W-Q-UfxBku09uNSth0rzMwMDHkLEIQ-eLzzqlM");

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

        [HttpPost("create")]
        public async Task<IActionResult> Create(PurchaseTicket purchaseTicket)
        {
            try
            {
                var purchaseTicketJson = JsonConvert.SerializeObject(purchaseTicket);

                var content = new StringContent(purchaseTicketJson, Encoding.UTF8, "application/json");

                var apiResponse = await _httpClient.PostAsync("https://localhost:7178/api/v1/purchase-ticket/create-purchase-ticket", content);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await apiResponse.Content.ReadAsStringAsync();
                    return StatusCode((int)apiResponse.StatusCode, errorMessage);
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}