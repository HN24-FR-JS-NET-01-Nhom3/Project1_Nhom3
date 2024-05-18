namespace LotteryChecker.Common.Models.Entities
{
    public class MonthlyStatistics
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int? Count { get; set; }
        public int? SumPrize { get; set; }
    }
}
