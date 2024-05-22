using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Infrastructures;
using LotteryChecker.Core.IRepositories;
using LotteryChecker.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Test
{
    public class PurchaseTicketRepositoryTest
    {
        private DbContextOptions<LotteryContext> _options;
        private LotteryContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<LotteryContext>()
                  .UseInMemoryDatabase(databaseName: "LotteryTest")
                  .Options;

            _context = new LotteryContext(_options);

            _context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetAllPurchaseTickets_ShouldReturnOrderedList_WhenTicketsWithDifferentDates()
        {
            var repository = new PurchaseTicketRepository(_context);

            var ticket1 = new PurchaseTicket
            {
                PurchaseTicketId = 1,
                PurchaseDate = DateTime.Now.AddDays(-7),
                LotteryNumber = "123456",
                DrawDate = DateTime.Now.AddDays(7),
                UserId = Guid.NewGuid(),
                User = new AppUser { Id = Guid.NewGuid(), UserName = "TestUser1", LastName = "Test" }
            };

            var ticket2 = new PurchaseTicket
            {
                PurchaseTicketId = 2,
                PurchaseDate = DateTime.Now.AddDays(-3),
                LotteryNumber = "654321",
                DrawDate = DateTime.Now.AddDays(10),
                UserId = Guid.NewGuid(),
                User = new AppUser { Id = Guid.NewGuid(), UserName = "TestUser2", LastName = "Test" }
            };

            var ticket3 = new PurchaseTicket
            {
                PurchaseTicketId = 3,
                PurchaseDate = DateTime.Now,
                LotteryNumber = "987654",
                DrawDate = DateTime.Now.AddDays(14),
                UserId = Guid.NewGuid(),
                User = new AppUser { Id = Guid.NewGuid(), UserName = "TestUser3", LastName = "Test" }
            };

            _context.PurchaseTickets.AddRange(ticket1, ticket2, ticket3);
            _context.SaveChanges();

            var result = repository.GetAll();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(3, result.First().PurchaseTicketId);
            Assert.AreEqual(2, result.Skip(1).First().PurchaseTicketId);
            Assert.AreEqual(1, result.Last().PurchaseTicketId);

        }
    }
}