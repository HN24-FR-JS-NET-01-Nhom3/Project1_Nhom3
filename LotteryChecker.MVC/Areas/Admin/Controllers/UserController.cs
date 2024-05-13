﻿using AutoMapper;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/user")]
[CustomAuthorize("Admin, Contributor")]
public class UserController : Controller
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }
        
    [HttpGet]
    [Route("get-all-users")]
    [Route("get-all-users/{page}/{pageSize}")]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
    {
        try
        {
            var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                $"{Constants.API_USER}/get-all-users/page={page}&pageSize={pageSize}", null, Request.Cookies["AccessToken"]);
            if (response.Data != null)
                return View(response.Data);
            else
                return View();
        }
        catch (Exception ex)
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
            var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                $"{Constants.API_USER}/get-user/{id}", null, Request.Cookies["AccessToken"]);
            if (response.Data?.Result != null)
                return View(response.Data.Result.FirstOrDefault());
            else
            {
                return View();
            }
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
            var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
                $"{Constants.API_USER}/get-user/{id}", null, Request.Cookies["AccessToken"]);
            if (response.Data?.Result != null)
                return View(response.Data.Result.FirstOrDefault());
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
    public async Task<IActionResult> Edit(UserVm? userVm, Guid id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Put,
                    $"{Constants.API_USER}/update-user/{id}", userVm, Request.Cookies["AccessToken"]);
                if (response != null)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
            return View();
        }
        catch (Exception ex)
        {
            return View();
        }

    }

    [Route("create-user")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("create-user")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserVm? userVm)
    {
        try
        {
            if(ModelState.IsValid)
            {
                var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Post,
                    $"{Constants.API_USER}/create-user", userVm, Request.Cookies["AccessToken"]);
                if (response != null)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    [HttpPost]
    [Route("change-status-user/{id}/{isActive}")]
    public async Task<IActionResult> ChangeStatusUser(string id, bool isActive)
    {
        try
        {
            var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Post,
                $"{Constants.API_USER}/update-status-user/{id}/{!isActive}", null, Request.Cookies["AccessToken"]);
            if (response != null)
            {
                TempData["SuccessMessage"] = "Changed status successfully";
                Console.WriteLine("ok");
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = JsonConvert.DeserializeObject<ErrorVm>(ex.Message);
            return RedirectToAction("Index");
        }
    }
}