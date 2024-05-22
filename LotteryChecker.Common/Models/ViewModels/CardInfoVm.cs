namespace LotteryChecker.Common.Models.ViewModels;

public class CardInfoVm
{
	public string Title { get; set; }
	public int Value { get; set; }
	public int DifferentValueWithLastMonth { get; set; }
	public double DifferentPercentWithlastMonth { get; set; }
}