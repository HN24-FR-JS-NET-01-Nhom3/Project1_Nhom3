namespace LotteryChecker.API.Models.Authentication;

public class AuthResultVm
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiresAt { get; set; }
}