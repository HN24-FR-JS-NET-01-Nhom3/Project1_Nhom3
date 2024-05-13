namespace LotteryChecker.Common.Models.Http;

public class Data<TEntity> where TEntity : class
{
	public IEnumerable<TEntity>? Result { get; set; }
	public Meta? Meta { get; set; }
}