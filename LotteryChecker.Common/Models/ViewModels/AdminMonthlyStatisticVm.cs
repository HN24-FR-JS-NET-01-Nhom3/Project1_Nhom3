namespace LotteryChecker.Common.Models.ViewModels;

public class AdminMonthlyStatisticVm
{
	public int Month { get; set; }
	public int Year { get; set; }
	public int LotteryCount { get; set; }
	public int PurchaseCount { get; set; }
	public int SearchCount { get; set; }
}