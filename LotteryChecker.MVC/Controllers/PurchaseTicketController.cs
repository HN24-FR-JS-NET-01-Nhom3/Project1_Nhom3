using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;

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
    
                var apiResponse = await _httpClient.GetAsync("https://localhost:7178/api/v1/purchase-ticket/get-all-purchase-tickets");

                if (apiResponse.IsSuccessStatusCode)
                {
           
                    var responseContent = await apiResponse.Content.ReadAsStringAsync();

          
                    var response = JsonConvert.DeserializeObject<HttpResponse<PurchaseTicketVm>>(responseContent);

             
                    return View(response.Result);
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
