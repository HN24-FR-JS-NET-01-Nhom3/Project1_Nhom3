using AutoMapper;
using LotteryChecker.Common.Models.Authentications;
using LotteryChecker.Common.Models.ViewModels;
using LotteryChecker.MVC.Models;
using LotteryChecker.MVC.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LotteryChecker.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/user")]
public class UserController : Controller
{
	private readonly IMapper _mapper;

	public UserController(IMapper mapper)
	{
		_mapper = mapper;
	}

	[HttpGet]
	[Route("")]
	[Route("{page}/{pageSize}")]
	[CustomAuthorize("Admin")]
	public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
	{
		try
		{
			if (Request.Cookies["User"] != null)
			{
				var user = JsonConvert.DeserializeObject<UserVm>(Request.Cookies["User"]);
				TempData["UserId"] = user.Id;
			}
			var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
				$"{Constants.API_USER}/get-all-users/page={page}&pageSize={pageSize}", null,
				Request.Cookies["AccessToken"]);
			if (response.Data != null)
				return View(response.Data);
			else
			{
				TempData["Errors"] = response.Errors;
				return View();
			}
		}
		catch (Exception ex)
		{
			TempData["Errors"] = ex.Message;
			return View();
		}
	}

	[Route("get-user/{id}")]
	[CustomAuthorize("Admin")]
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
				TempData["Errors"] = response.Errors;
				return RedirectToAction("Index");
			}
		}
		catch (Exception ex)
		{
			TempData["Errors"] = ex.Message;
			return RedirectToAction("Index");
		}
	}

	[HttpGet]
	[Route("edit-user/{id}")]
	[CustomAuthorize("Admin")]
	public async Task<IActionResult> Edit(Guid id)
	{
		try
		{
			var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Get,
				$"{Constants.API_USER}/get-user/{id}", null, Request.Cookies["AccessToken"]);
			if (response.Data?.Result != null)
			{
				var roleResponse = await HttpUtils<IdentityRole<Guid>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_USER}/get-all-roles", null, Request.Cookies["AccessToken"]);
				var selectRole = roleResponse.Data?.Result
					?.FirstOrDefault(role => role.Name == response.Data?.Result?.FirstOrDefault()?.Role)?.Name;
				if (roleResponse.Errors == null)
				{
					ViewBag.Roles = new SelectList(
						roleResponse.Data?.Result,
						"Name", 
						"Name",
						selectRole
					);
				}
				return View(response.Data.Result.FirstOrDefault());
			}
			else
			{
				TempData["Errors"] = response.Errors;
				return RedirectToAction("Index");
			}
		}
		catch (Exception ex)
		{
			TempData["Errors"] = ex.Message;
			return RedirectToAction("Index");
		}
	}

	[HttpPost]
	[Route("edit-user/{id}")]
	[CustomAuthorize("Admin")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(UserVm? userVm, Guid id)
	{
		try
		{
			if (ModelState.IsValid)
			{
				
				var roleResponse = await HttpUtils<IdentityRole<Guid>>.SendRequest(HttpMethod.Get,
					$"{Constants.API_USER}/get-all-roles", null, Request.Cookies["AccessToken"]);
				var selectRole = roleResponse.Data?.Result
					?.FirstOrDefault(role => role.Name == userVm?.Role);

				userVm.Role = selectRole.ToString();
				
				var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Put,
					$"{Constants.API_USER}/update-user/{id}", userVm, Request.Cookies["AccessToken"]);
				if (response.Errors == null)
				{
					TempData["Messages"] = "Updated successfully!";
					return RedirectToAction("Index");
				}
				else
				{
					TempData["Errors"] = response.Errors;
					return RedirectToAction("Index");
				}
			}

			return View();
		}
		catch (Exception ex)
		{
			TempData["Errors"] = ex.Message;
			return View();
		}
	}

	[Route("create-user")]
	[CustomAuthorize("Admin")]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[Route("create-user")]
	[ValidateAntiForgeryToken]
	[CustomAuthorize("Admin")]
	public async Task<IActionResult> Create(CreateUserVm? userVm)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Post,
					$"{Constants.API_USER}/create-user", userVm, Request.Cookies["AccessToken"]);
				if (response.Data != null)
				{
					TempData["Messages"] = "Created successfully!";
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var error in response.Errors ?? [])
					{
						ModelState.AddModelError(string.Empty, error);
					}

					return View();
				}
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
	[CustomAuthorize("Admin")]
	public async Task<IActionResult> ChangeStatusUser(string id, bool isActive)
	{
		try
		{
			var response = await HttpUtils<UserVm>.SendRequest(HttpMethod.Post,
				$"{Constants.API_USER}/update-status-user/{id}/{!isActive}", null, Request.Cookies["AccessToken"]);
			if (response.Data != null)
			{
				TempData["Messages"] = "Changed status successfully";
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

	[HttpGet]
	[Route("change-password")]
	[CustomAuthorize("Admin, User")]
	public IActionResult ChangePassword()
	{
		return View();
	}

	[HttpPost]
	[Route("change-password")]
	[CustomAuthorize("Admin, User")]
	public async Task<IActionResult> ChangePassword(ChangePasswordVm changePasswordVm)
	{
		try
		{
			if (ModelState.IsValid)
			{
				var response = await HttpUtils<string>.SendRequest(HttpMethod.Post,
					$"{Constants.API_AUTHEN}/change-password", changePasswordVm, Request.Cookies["AccessToken"]);
				if (response.Message != null)
				{
					TempData["Messages"] = "Change password successfully!";
					return RedirectToAction("Index", "HomeAdmin");
				}

				if (response.Errors != null)
				{
					foreach (var error in response.Errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}

					return View();
				}
			}

			return View();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
	
	[HttpPost]
	[Route("export-user")]
	public async Task<IActionResult> ExportUsers([FromForm] List<UserVm> userVms)
	{
		try
		{
			var response = await HttpUtils<FileResultVm>.SendRequest(HttpMethod.Post,
				$"{Constants.API_USER}/excel-export", userVms);

			if (response.Errors == null)
			{
				var fileResult = response.Data?.Result?.FirstOrDefault();
				if (fileResult != null)
				{
					var fileBytes = Convert.FromBase64String(fileResult.FileContents);

					return File(fileBytes, fileResult.ContentType, fileResult.FileDownloadName);
				}
			}

			// Handle case where there are errors or no file result
			TempData["ErrorMessage"] = "An error occurred while exporting the file.";
			return RedirectToAction("Index");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw;
		}
	}
}