using AutoMapper;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Controllers
{
    public class SearchHistoryController : BaseController
    {
        public SearchHistoryController(IMapper _mapper) : base(_mapper)
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (TempData["User"] != null)
                {
                    var userData = TempData["User"].ToString();
                    var user = JsonConvert.DeserializeObject<UserVm>(userData);
                    if (user != null)
                    {
                        var searchHistoryResponse = await HttpUtils<IEnumerable<SearchTicketVm>>.SendRequest(HttpMethod.Get,
                        $"{Constants.API_SEARCH_HISTORY}/get-search-histories-by-user-id?userId={user.Id}");
                        await Console.Out.WriteLineAsync(searchHistoryResponse.ToString());
                        return View(searchHistoryResponse);
                    }
                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}
