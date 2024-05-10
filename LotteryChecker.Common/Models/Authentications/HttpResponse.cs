namespace LotteryChecker.Common.Models.Authentications;

public class HttpResponse<TEntity> where TEntity : class
{
	public IEnumerable<TEntity> Result { get; set; }
	public Meta Meta { get; set; }
}