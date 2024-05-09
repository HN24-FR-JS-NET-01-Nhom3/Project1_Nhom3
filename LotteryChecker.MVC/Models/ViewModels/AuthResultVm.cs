using LotteryChecker.MVC.Models.Entities;

namespace LotteryChecker.MVC.Models.ViewModels;

public class AuthResultVm
{
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiresAt { get; set; }
	public UserVm User { get; set; }
}