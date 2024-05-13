using System.Net;

namespace LotteryChecker.Common.Models.Http;

public class Response<TEntity> where TEntity : class
{
	public Data<TEntity>? Data { get; set; }
	public IEnumerable<string>? Errors { get; set; }
	public string? Message { get; set; }
}