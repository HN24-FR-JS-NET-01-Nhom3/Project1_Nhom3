﻿using AutoMapper;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [Route("get-all-users")]
        public async Task<IActionResult> ListUser()
        {
            try
            {
                var response = await HttpUtils<List<UserVm>>.SendRequest(HttpMethod.Get,
                $"{Constants.API_USER}/get-all-users");
                if (response != null)
                    return View(response);
                else
                    return View();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        [Route("get-user/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var respone = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                    $"{Constants.API_USER}/get-user/{id}");
                if (respone != null)
                    return View(respone);
                else
                    return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

            [HttpGet]
            [Route("edit-user/{id}")]
            public async Task<IActionResult> Edit(Guid id)
            {
                try
                {
                    var respone = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                        $"{Constants.API_USER}/get-user/{id}");
                    if (respone != null)
                        return View(respone);
                    else
                        return View();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return View();
                }
                }

            [HttpPost]
            [Route("edit-user/{id}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UserVm userVm, Guid id)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var respone = await HttpUtils<UserVm>.SendRequest(HttpMethod.Put,
                             $"{Constants.API_USER}/update-user/{id}", JsonConvert.SerializeObject(userVm));
                        if (respone != null)
                        {
                            return RedirectToAction("ListUser");
                        }
                        else
                            return View();
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return View();
                }
            
            }
    }
}