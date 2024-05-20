namespace LotteryChecker.MVC.Utils
{
    public static class DateTimeHelper
    {
        public static string GetDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
