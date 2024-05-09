using LotteryChecker.API.Models.Entities;

namespace LotteryChecker.API.Models.Authentication;

public class AuthResultVm
{
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiresAt { get; set; }
	public UserVm User { get; set; }
}