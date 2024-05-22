namespace LotteryChecker.Common.Models.ViewModels;

public class StatisticVm
{
	public UserVm User { get; set; }
	public CardInfoVm PurchaseCount { get; set; }
	public CardInfoVm WinCount { get; set; }
	public CardInfoVm MoneyWinCount { get; set; }
	public IEnumerable<MonthlyStatisticVm> MonthlyStatistics { get; set; }
}