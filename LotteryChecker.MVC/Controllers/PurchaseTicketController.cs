using AutoMapper;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Models.Entities;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.MVC.Controllers
{
    [Route("PurchaseTicket")]
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
                var purchaseTicketResponse = await HttpUtils<IEnumerable<PurchaseTicketVm>>.SendRequestAndProcessResponse(HttpMethod.Get, $"{Constants.API_PURCHASE_TICKET}/get-all-purchase-tickets");

                if (purchaseTicketResponse == null) purchaseTicketResponse = [];
                var purchaseTicketResult = purchaseTicketResponse.ToList();

                return View(purchaseTicketResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}
