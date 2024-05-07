using LotteryChecker.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace LotteryChecker.API.Helpers;

public class LotteryQuery
{
	public string? Company { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public bool? Expired { get; set; }
	public bool? Published { get; set; }
	public IEnumerable<Func<Lottery, int, bool>> Filters
	{
		get 
		{
			var filters = new List<Func<Lottery, int, bool>>();

			if (!Company.IsNullOrEmpty())
				filters.Add((p, _) => (p.Company ?? "").ToLower().Contains(Company.ToLower()));
			if (StartDate.HasValue)
				filters.Add((p, _) => p.PublishDate > StartDate);
			if (EndDate.HasValue)
				filters.Add((p, _) => p.DrawDate.AddDays(30) > EndDate);
			if (Expired.HasValue)
				filters.Add((p, _) => p.DrawDate.AddDays(30) < DateTime.Now);
			if (Published.HasValue)
				filters.Add((p, _) => p.IsPublished == Published);

			return filters;
		}
	}
}