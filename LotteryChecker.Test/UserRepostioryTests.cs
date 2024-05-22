using LotteryChecker.Core.Data;
using LotteryChecker.Core.Entities;
using LotteryChecker.Core.Enums;
using LotteryChecker.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Test
{
    public class UserRepositoryTests
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
        public void FindUserByPhone_ShouldReturnUserWithMatchingPhoneNumber()
        {
            var repository = new UserRepository(_context);
            // Arrange
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Hiu",
                LastName = "Ho",
                LastLogin = DateTime.Now,
                PhoneNumber = "1234567890",
                Email = "test@example.com"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = repository.FindUserByPhone("1234567890");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.PhoneNumber, result.PhoneNumber);
        }

        [Test]
        public void FindUserByEmail_ShouldReturnUserWithMatchingEmail()
        {
            var repository = new UserRepository(_context);
            // Arrange
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Hiu",
                LastName = "Ho",
                LastLogin = DateTime.Now,
                PhoneNumber = "1234567890",
                Email = "HieuHV21@fpt.com"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = repository.FindUserByEmail("HieuHV21@fpt.com");
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public void FilterUserIsActive_ShouldReturnInactiveUsersBasedOnTimeFrameAndTimeUnit()
        {
            // Arrange
            var repository = new UserRepository(_context);

            var activeUser = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Hiu",
                LastName = "Ho",
                PhoneNumber = "1234567890",
                Email = "HieuHV21@fpt.com",
                LastLogin = DateTime.Now.AddDays(-5)
            };

            var inactiveUser1 = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Hiu",
                LastName = "chi",
                PhoneNumber = "1234567890",
                Email = "Hieuhc2@fpt.com",
                LastLogin = DateTime.Now.AddMonths(-2)
            };

            var inactiveUser2 = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Vit",
                LastName = "Lq",
                PhoneNumber = "1234567890",
                Email = "VietLq@fpt.com",
                LastLogin = DateTime.Now.AddYears(-1)
            };

            _context.Users.AddRange(activeUser, inactiveUser1, inactiveUser2);
            _context.SaveChanges();

            // Act
            var result = repository.FilterUserIsActive(2, TimeUnit.Month);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.Contains(inactiveUser1, result);
            Assert.Contains(inactiveUser2, result);
            Assert.IsFalse(result.Contains(activeUser));
        }
    }
}
