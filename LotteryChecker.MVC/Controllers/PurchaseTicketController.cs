using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
using LotteryChecker.Core.Entities;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Controllers
{
    [Route("purchase-ticket")]
    public class PurchaseTicketController : Controller
    {
        private readonly IMapper _mapper;

        public PurchaseTicketController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var purchaseTicketResponse = await HttpUtils<Response<PurchaseTicketVm>>.SendRequest(HttpMethod.Get, $"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets");
                ViewData["PurchaseTickets"] = purchaseTicketResponse;
                
                return View();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseTicket(PurchaseTicketVm? purchaseTicketVm)
        {
            if (ModelState.IsValid)
            {
                if (purchaseTicketVm == null)
                {
                    return View("Index");
                }

                try
                {
                    var purchaseResponse = await HttpUtils<PurchaseTicket>.SendRequest(HttpMethod.Post,
                        $"{Constants.API_PURCHASE_TICKET}/create-purchase-ticket", purchaseTicketVm);
                    if (purchaseResponse != null)
                    {
                        return View("Index");
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            return View("Index");
        }
    }
}
