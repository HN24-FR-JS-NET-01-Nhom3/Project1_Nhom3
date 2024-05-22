using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;

namespace LotteryChecker.Core.IRepositories;

public interface ISearchHistoryRepository : IBaseRepository<SearchHistory>
{
    public IEnumerable<SearchHistory> GetByUserId(Guid userId);
}