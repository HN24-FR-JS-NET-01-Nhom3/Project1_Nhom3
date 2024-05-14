namespace LotteryChecker.Core.Entities;

public class RefreshToken
{
	public int Id { get; set; }
	public string Token { get; set; }
	public string JwtId { get; set; }
	public bool IsRevoked { get; set; }
	public DateTime AddDate { get; set; }
	public DateTime ExpireDate { get; set; }
	public Guid UserId { get; set; } 
	public AppUser User { get; set; }
}