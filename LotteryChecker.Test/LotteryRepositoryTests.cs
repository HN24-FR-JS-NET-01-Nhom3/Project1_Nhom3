using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Test
{
    [TestFixture]
    public class LotteryRepositoryTests
    {
        private DbContextOptions<LotteryContext> _options;
        private LotteryContext _context;
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<LotteryContext>()
                .UseInMemoryDatabase(databaseName: "LotteryTestDatabase")
                .Options;
            _context = new LotteryContext(_options);

            // Clear existing data (if any) before each test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
        [Test]
        public void GenerateLotteryResult_ShouldCreateExpectedNumberOfLotteries()
        {
            // Arrange
            var repository = new LotteryRepository(_context);
            var drawDate = new DateTime(2024, 5, 22);

            // Act
            var results = repository.GenerateLotteryResult(drawDate);

            // Assert
            Assert.AreEqual(17, results.Count());
            Assert.AreEqual(1, results.Count(r => r.RewardId == 8));
            Assert.AreEqual(1, results.Count(r => r.RewardId == 7));
            Assert.AreEqual(3, results.Count(r => r.RewardId == 6));
            Assert.AreEqual(1, results.Count(r => r.RewardId == 5));
            Assert.AreEqual(7, results.Count(r => r.RewardId == 4));
            Assert.AreEqual(2, results.Count(r => r.RewardId == 3));
            Assert.AreEqual(1, results.Count(r => r.RewardId == 2));
            Assert.AreEqual(1, results.Count(r => r.RewardId == 1));
        }

        [Test]
        public void GetLotteryResult_ShouldReturnResultsForGivenDate()
        {
            // Arrange
            var repository = new LotteryRepository(_context);
            var drawDate = new DateTime(2024, 5, 22);
            var lotteries = new List<Lottery>
            {
                new Lottery { DrawDate = drawDate, PublishDate = drawDate.AddDays(-3), LotteryNumber = "01", IsPublished = true, RewardId = 8 },
                new Lottery { DrawDate = drawDate, PublishDate = drawDate.AddDays(-3), LotteryNumber = "123", IsPublished = true, RewardId = 7 },
                new Lottery { DrawDate = drawDate, PublishDate = drawDate.AddDays(-3), LotteryNumber = "4567", IsPublished = true, RewardId = 6 },
                new Lottery { DrawDate = drawDate.AddDays(1), PublishDate = drawDate.AddDays(-3), LotteryNumber = "89", IsPublished = true, RewardId = 8 } // Different date
            };

            _context.Lotteries.AddRange(lotteries);
            _context.SaveChanges();

            // Act
            var results = repository.GetLotteryResult(drawDate).ToList();

            // Assert   
            Assert.AreEqual(3, results.Count());
            Assert.IsTrue(results.All(r => r.DrawDate == drawDate));
        }

        [Test]
        public void UnpublishExpiredLotteries_ShouldSetIsPublishedToFalseForExpiredLotteries()
        {
            // Arrange
            var repository = new LotteryRepository(_context);
            var drawDate = DateTime.Now.AddDays(-31);
            repository.GenerateLotteryResult(drawDate);
            var anotherDrawDate = DateTime.Now.AddDays(-20);
            repository.GenerateLotteryResult(anotherDrawDate);

            // Act
            repository.UnpublishExpiredLotteries();
            _context.SaveChanges();

            // Assert
            var expiredLotteries = _context.Lotteries.Where(l => l.DrawDate == drawDate).ToList();
            var validLotteries = _context.Lotteries.Where(l => l.DrawDate == anotherDrawDate).ToList();

            Assert.IsTrue(expiredLotteries.All(l => l.IsPublished == false));
            Assert.IsTrue(validLotteries.All(l => l.IsPublished == true));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}