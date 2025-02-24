﻿using DAL.DatabaseContextNamespace;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
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
        public void Test_CreateCorrectUser()
        {
            int countUsersBeforeCreating = _dbContext.Users.Count();


            Random random = new();
            int randomNumber = random.Next(1, 100001);
            string uniqueEmail = $"dimochka{randomNumber}@gmail.com";

            UserDb correctUser= new() {Nickname = "Dima", Email = uniqueEmail,Password = $"54u3y5u35h{randomNumber}" };
            Assert.DoesNotThrow(() => _repository.Create(correctUser));

            int countUsersAfterCreating = _dbContext.Users.Count();

            Assert.That(countUsersBeforeCreating, Is.LessThanOrEqualTo(countUsersAfterCreating));
            Assert.That(countUsersBeforeCreating + 1, Is.EqualTo(countUsersAfterCreating));
        }

        [Test]
        public void Test_CreateIncorrectUser()
        {
            UserDb userNull = new() { Nickname = null!, Email = null!, Password = null! };
            UserDb userNotFull = new() { Nickname = "dddd", Email=null!,Password=null! };
            UserDb userIncorrectEmail = new() {Email="dddd",Nickname="Ahome",Password="02302408942" };


            Assert.Throws<ArgumentNullException>(() => _repository.Create(userNull));
            Assert.Throws<DbUpdateException>(() => _repository.Create(userNotFull));
            Assert.Throws<DbUpdateException>(() => _repository.Create(userIncorrectEmail));
        }

        [Test]
        public void Test_UpdateSuccess() 
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100001);


            var idFirstUser = _repository.GetAll()[0].Id;
            UserDb correctUserUpdate = new() {Email = $"tuutut{randomNumber}@gmail.com", Nickname=null!, Password=null!};

            Assert.DoesNotThrow(() => _repository.Update(idFirstUser, correctUserUpdate));

            var updatedUserEmail = _repository.GetAll()[0].Email;
            Assert.That(correctUserUpdate.Email, Is.EqualTo(updatedUserEmail));
        }

        [Test]
        public void Test_UpdateFailure()
        {
            var idFirstUser = _repository.GetAll()[0].Id;
            var firstUser = _repository.GetAll()[0];

            UserDb nullUser = null!;
            UserDb emptyUser = new UserDb() {Nickname=null!, Email= null!, Password = null! };

            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, nullUser!));
            Assert.Throws<ArgumentNullException>(() => _repository.Update(idFirstUser, emptyUser));

            Assert.Throws<ArgumentException>(
                () => _repository.Update(Guid.NewGuid(), firstUser)
            );
        }

        [Test] 
        public void Test_DeleteSuccess()
        {
            var idDeleteUser = _repository.GetAll()[^1].Id;
            var countUsersBefore = _repository.GetAll().Count;

            Assert.DoesNotThrow(() => _repository.Delete(idDeleteUser));

            var countUsersAfter = _repository.GetAll().Count;

            Assert.That(countUsersBefore - 1, Is.EqualTo(countUsersAfter));
        }

        [Test]
        public void Test_DeleteFailure()
        {
            Assert.Throws<ArgumentException>(() => _repository.Delete(Guid.NewGuid()));
        }

        [Test]
        public void Test_GetSuccess()
        {
            var firstUser = _repository.GetAll()[0];
            var idFirstUser = firstUser.Id;

            Assert.DoesNotThrow(() => _repository.Get(idFirstUser));

            var foundUser = _repository.Get(idFirstUser);

            Assert.That(foundUser, !Is.Null);
            Assert.That(firstUser.Email, Is.EqualTo(foundUser.Email));
            Assert.That(firstUser.Password, Is.EqualTo(foundUser.Password));
        }

        [Test]
        public void Test_GetFailure()
        {
            var notExistUserId = Guid.NewGuid();

            Assert.Throws<InvalidOperationException>(() => _repository.Get(notExistUserId));
        }
    }
}
