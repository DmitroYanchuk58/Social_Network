using BAL.Services;
using DAL.DatabaseContextNamespace;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using UserDb = DAL.Entities.User;
using UserHelper = DAL.Helpers.EntityHelpers.UserHelper;

namespace Tests.BAL_Tests
{
    public class AuthServiceTests
    {
        private CrudRepository<UserDb> _crudRepository;
        private AuthService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            var dbContext = new DatabaseContext(options);
            IEntityHelper<UserDb> helper = new UserHelper();
            _crudRepository = new CrudRepository<UserDb>(dbContext, helper);
            _service = new AuthService(dbContext);
        }

        [Test]
        public void Test_Register_Success()
        {
            Random random = new();
            int randomNumber = random.Next(1, 100001);

            var countUsersBefore = _crudRepository.GetAll().Count;

            var email = $"jtrjryo{randomNumber}@gmail.com";
            var password = $"6ty{randomNumber}j4yjy54{randomNumber}";
            var nickname = $"fkefee{randomNumber}kerfk";

            Assert.DoesNotThrow(
                () => _service.Registration(email, password, nickname)
            );

            var countUsersAfter = _crudRepository.GetAll().Count;

            Assert.That(countUsersBefore, Is.EqualTo(countUsersAfter - 1));
        }

        [Test]
        public void Test_Register_Failure()
        {
            Assert.Throws<ArgumentNullException>(
                () => _service.Registration(null!, null!, null!)
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
            var email = "admin63@gmail.com";
            var password = "admin";
            var isUserExist = _service.Authentication(email, password);

            Assert.That(isUserExist, Is.True);
        }

        [Test]
        public void Test_Authentication_Failure()
        {
            var wrongPassword = "3434";
            var wrongEmail = "1111@gmail.com";
            var emailWithoutGmail = "rhhefg";
            var emailEmpty = "";
            string emailNull = null!;
            string nullPassword = null!;

            var isUserExist = _service.Authentication(wrongEmail, wrongPassword);
            var isUserExist2 = _service.Authentication(emailWithoutGmail, wrongPassword);

            Assert.That(isUserExist, Is.False); 
            Assert.That(isUserExist2, Is.False);

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
