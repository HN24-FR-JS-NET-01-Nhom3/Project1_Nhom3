using LotteryChecker.Common.Models.ViewModels;

namespace LotteryChecker.Common.Models.Authentications;

public class AuthResultVm
{
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiresAt { get; set; }
	public UserVm User { get; set; }
}