using BAL.Services;
using DAL.DatabaseContextNamespace;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using UserDb = DAL.Entities.User;
using UserHelper = DAL.Helpers.EntityHelpers.UserHelper;

namespace Tests.BAL_Tests
{
    public class UserServiceTests
    {
        private CrudRepository<UserDb> _crudRepository;
        private UserService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            var dbContext = new DatabaseContext(options);
            IEntityHelper<UserDb> helper = new UserHelper();
            _crudRepository = new CrudRepository<UserDb>(dbContext, helper);
            _service = new UserService(dbContext);
        }

        [Test]
        public void Test_Register_Success()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100001);

            var countUsersBefore = _crudRepository.GetAll().Count;

            var email = $"jtrjryo{randomNumber}@gmail.com";
            var password = $"6ty{randomNumber}j4yjy54{randomNumber}";
            var nickname = $"fkefee{randomNumber}kerfk";

            Assert.DoesNotThrow(
                () => _service.Registration(email, password, nickname)
            );

            var countUsersAfter = _crudRepository.GetAll().Count;

            Assert.That(countUsersAfter - 1, Is.EqualTo(countUsersBefore));
        }

        [Test]
        public void Test_Register_Failure()
        {
            Assert.Throws<ArgumentException>(
                () => _service.Registration(null, null, null)
            );
            Assert.Throws<ArgumentException>(
                () => _service.Registration("", "", "")
            );
            Assert.Throws<ArgumentException>(
                () => _service.Registration(" ", " ", " ")
            );
            Assert.Throws<ArgumentException>(
                () => _service.Registration("teteete", "rwrr23", "ok546ioj5y")
            );
        }

        [Test]
        public void Test_Authentication_Success()
        {
            var email = "dima555588@gmail.com";
            var password = "admin";
            var isUserExist = _service.Authentication(email, password);

            Assert.That(isUserExist, Is.EqualTo(true));
        }

        [Test]
        public void Test_Authentication_Failure()
        {
            var wrongPassword = "3434";
            var wrongEmail = "1111@gmail.com";
            var emailWithoutGmail = "rhhefg";
            var emailEmpty = "";
            string emailNull = null;
            string nullPassword = null;

            var isUserExist = _service.Authentication(wrongEmail, wrongPassword);
            var isUserExist2 = _service.Authentication(emailWithoutGmail, wrongPassword);

            Assert.That(isUserExist, Is.EqualTo(false));
            Assert.That(isUserExist2, Is.EqualTo(false));

            Assert.Throws<ArgumentNullException>(
                () => _service.Authentication(emailEmpty, wrongPassword)
            );
            Assert.Throws<ArgumentNullException>(
                () => _service.Authentication(emailNull, wrongPassword)
            );
            Assert.Throws<ArgumentNullException>(
                () => _service.Authentication(emailNull, nullPassword)
            );
        }
    }
}
