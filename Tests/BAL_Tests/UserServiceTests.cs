using BAL.Helpers;
using BAL.Services;
using DAL.DatabaseContextNamespace;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _service = new UserService(_crudRepository);
        }

        [Test]
        public void Test_Register_Success()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100001);

            var countUsersBefore = _crudRepository.GetAll().Count();

            var email = $"edeede{randomNumber}@gmail.com";
            var password = $"pas{randomNumber}sword";
            var nickname = $"fkefee{randomNumber}kerfk";

            Assert.DoesNotThrow(
                () => _service.Registration(email, password, nickname)
            );

            var countUsersAfter = _crudRepository.GetAll().Count();

            Assert.IsTrue(countUsersBefore == countUsersAfter - 1);
        }

        [Test]
        public void Test_Register_Failure()
        {
            Assert.Throws<ArgumentNullException>(
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
    }
}
