using LotteryChecker.Core.Entities;
using LotteryChecker.Core.IRepositories;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.Data;

namespace LotteryChecker.Core.Repositories;

public class SearchHistoryRepository : BaseRepository<SearchHistory>, ISearchHistoryRepository
{
    public SearchHistoryRepository(LotteryContext context) : base(context)
    {
        }

    public IEnumerable<SearchHistory> GetByUserId(Guid userId)
    {
        return Find(x => x.UserId == userId).ToList();
    }
}