using Microsoft.AspNetCore.Mvc;

namespace LotteryChecker.MVC.Areas.Admin.Models;

public class AdminCardInfoVm
{
	public string Title { get; set; }
	public int Count { get; set; }
	public string ControllerName { get; set; }
}