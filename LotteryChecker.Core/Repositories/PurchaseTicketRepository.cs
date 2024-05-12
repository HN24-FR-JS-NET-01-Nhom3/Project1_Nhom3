using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;

namespace LotteryChecker.Core.Repositories;

public class PurchaseTicketRepository : BaseRepository<PurchaseTicket>, IPurchaseTicketRepository
{
    public PurchaseTicketRepository(LotteryContext context) : base(context)
    {
        }

    public IEnumerable<PurchaseTicket> GetAllPurchaseTickets()
    {
        return DbSet;
    }
}