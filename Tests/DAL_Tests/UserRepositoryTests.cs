using BAL.DTOs;
using DAL.DatabaseContextNamespace;
using DAL.Entities;
using DAL.Helpers.EntityHelpers;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDb = DAL.Entities.User;
using UserHelperDB = DAL.Helpers.EntityHelpers.UserHelper;

namespace Tests.DAL_Tests
{
    public class UserRepositoryTests
    {
        private DatabaseContext _dbContext;
        private CrudRepository<UserDb> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            _dbContext = new DatabaseContext(options);
            IEntityHelper<UserDb> helper = new UserHelperDB();
            _repository = new CrudRepository<UserDb>(_dbContext, helper);
        }

        [TearDown]
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Test]
        [Repeat(100)]
        public void Test_CreateCorrectUser()
        {
            int countUsersBeforeCreating = _dbContext.Users.Count();


            Random random = new Random();
            int randomNumber = random.Next(1, 100001);
            string uniqueEmail = $"dimochka{randomNumber}@gmail.com";

            UserDb correctUser= new UserDb() {Nickname = "Dima", Email = uniqueEmail,Password = $"54u3y5u35h{randomNumber}" };
            Assert.DoesNotThrow(() => _repository.Create(correctUser));

            int countUsersAfterCreating = _dbContext.Users.Count();

            Assert.IsTrue(countUsersBeforeCreating != countUsersAfterCreating);
            Assert.IsTrue(countUsersBeforeCreating+1 == countUsersAfterCreating);
        }

        [Test]
        public void Test_CreateIncorrectUser()
        {
            UserDb userNull = new UserDb() { Nickname=null, Email=null, Password=null };
            UserDb userNotFull = new UserDb() { Nickname = "dddd", Email=null, Password=null };
            UserDb userIncorrectEmail = new UserDb() { Email="dddd",Nickname="Ahome",Password="02302408942" };

            Assert.Throws<ArgumentNullException>(() => _repository.Create(userNull));
            Assert.Throws<DbUpdateException>(() => _repository.Create(userNotFull));
            Assert.Throws<DbUpdateException>(() => _repository.Create(userIncorrectEmail));
        }

        [Test]
        public void Test_UpdateSuccess() 
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100001);


            var idFirstUser = _repository.GetAll().First().Id;
            UserDb correctUserUpdate = new UserDb() {Email = $"tuutut{randomNumber}@gmail.com", Nickname=null, Password=null };

            Assert.DoesNotThrow(() => _repository.Update(idFirstUser, correctUserUpdate));

            var updatedUserEmail = _repository.GetAll().First().Email;
            Assert.IsTrue(String.Equals(correctUserUpdate.Email, updatedUserEmail));
        }

        [Test]
        public void Test_UpdateFailure()
        {
            var idFirstUser = _repository.GetAll().First().Id;
            var firstUser = _repository.GetAll()[0];

            UserDb nullUser = null;
            UserDb emptyUser = new UserDb() { Nickname=null, Email=null, Password = null };

            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, nullUser));
            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, emptyUser));

            Assert.Throws<ArgumentException>(
                () => _repository.Update(Guid.NewGuid(), firstUser)
            );
        }

        [Test] 
        public void Test_DeleteSuccess()
        {
            var idDeleteUser = _repository.GetAll().Last().Id;
            var countUsersBefore = _repository.GetAll().Count();

            Assert.DoesNotThrow(() => _repository.Delete(idDeleteUser));

            var countUsersAfter = _repository.GetAll().Count();

            Assert.IsTrue(countUsersBefore - 1 == countUsersAfter);
        }

        [Test]
        public void Test_DeleteFailure()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Delete(Guid.NewGuid()));
        }

        [Test]
        public void Test_GetSuccess()
        {
            var firstUser = _repository.GetAll().First();
            var idFirstUser = firstUser.Id;

            Assert.DoesNotThrow(() => _repository.Get(idFirstUser));

            var foundUser = _repository.Get(idFirstUser);

            Assert.IsNotNull(foundUser);
            Assert.IsTrue(firstUser.Email == foundUser.Email &&
                firstUser.Password == foundUser.Password);
        }

        [Test]
        public void Test_GetFailure()
        {
            var notExistUserId = Guid.NewGuid();

            Assert.Throws<InvalidOperationException>(() => _repository.Get(notExistUserId));
        }
    }
}
