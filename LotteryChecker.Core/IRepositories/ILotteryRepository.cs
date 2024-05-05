using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;

namespace LotteryChecker.Core.IRepositories;

public interface ILotteryRepository : IBaseRepository<Lottery>
{
    public IList<Lottery> GetLotteriesByCompany(string company);
    public IList<Lottery> GetLotteriesByDate(DateTime dateStart , DateTime dateEnd);
    public IList<Lottery> GetLotteriesByDateAndCompany(DateTime dateStart, DateTime dateEnd, string company);
    public IList<Lottery> GetLotteriesPublished();
    public IList<Lottery> GetLotteriesUnpublished();
    public IList<Lottery> GetLotteriesExpired();

}