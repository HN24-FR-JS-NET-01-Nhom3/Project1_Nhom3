namespace LotteryChecker.Common.Models.ViewModels;

public class MonthlyStatisticVm
{
	public int Month { get; set; }
	public int Year { get; set; }
	public int PurchaseCount { get; set; }
	public int WinCount { get; set; }
	public double MoneyWinCount { get; set; }
}
