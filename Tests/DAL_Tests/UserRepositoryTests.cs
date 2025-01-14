using DAL.DatabaseContextNamespace;
using DAL.Entities;
using DAL.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Tests
{
    public class UserRepositoryTests
    {
        private DatabaseContext _dbContext;
        private CrudRepository<User> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            _dbContext = new DatabaseContext(options);
            _repository = new CrudRepository<User>(_dbContext);
        }

        [TearDown]
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void Test_CreateCorrectUser()
        {
            int countUsersBeforeCreating = _dbContext.Users.Count();


            Random random = new Random();
            int randomNumber = random.Next(1, 100001);
            string uniqueEmail = $"dimochka{randomNumber}@gmail.com";

            User correctUser= new User() {Nickname="Dima", Email=uniqueEmail,Password="54u3y5u35h" };
            Assert.DoesNotThrow(() => _repository.Create(correctUser));

            int countUsersAfterCreating = _dbContext.Users.Count();

            Assert.IsTrue(countUsersBeforeCreating != countUsersAfterCreating);
            Assert.IsTrue(countUsersBeforeCreating+1 == countUsersAfterCreating);
        }

        [Test]
        public void Test_CreateIncorrectUser()
        {
            User userNull = new User();
            User userNotFull = new User() { Nickname = "dddd" };
            User userIncorrectEmail = new User() {Email="dddd",Nickname="Ahome",Password="02302408942" };

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
            User correctUserUpdate = new User() {Email = $"tuutut{randomNumber}@gmail.com" };

            Assert.DoesNotThrow(() => _repository.Update(idFirstUser, correctUserUpdate));

            var updatedUserEmail = _repository.GetAll().First().Email;
            Assert.IsTrue(String.Equals(correctUserUpdate.Email, updatedUserEmail));
        }

        [Test]
        public void Test_UpdateFailure()
        {
            var idFirstUser = _repository.GetAll().First().Id;

            User nullUser = null;
            User emptyUser = new User();

            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, nullUser));
            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, emptyUser));
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
