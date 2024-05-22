using AutoMapper;
using LotteryChecker.Common.Models.Entities;
using LotteryChecker.Common.Models.Http;
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
                        var searchHistoryResponse = await HttpUtils<SearchHistoryVm>.SendRequest(HttpMethod.Get,
                        $"{Constants.API_SEARCH_HISTORY}/get-search-histories-by-user-id",accessToken: Request.Cookies["AccessToken"]);
                        if (searchHistoryResponse.Errors == null)
                        {
                            return View(searchHistoryResponse.Data?.Result);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = searchHistoryResponse.Errors;
                            return View();
                        }    
                            
                    }
                }
                return RedirectToAction("Login", "Authen");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        public IActionResult Details()
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
