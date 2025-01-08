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
        private CRUD_Repository<User> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            _dbContext = new DatabaseContext(options);
            _repository = new CRUD_Repository<User>(_dbContext);
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
        public void Test_CreateIncorrectUSer()
        {
            User userNull = new User();

            User userNotFull = new User() { Nickname = "dddd" };

            User userIncorrectEmail = new User() {Email="dddd",Nickname="Ahome",Password="02302408942" };



            Assert.Throws<DbUpdateException>(() => _repository.Create(userNull));

            Assert.Throws<DbUpdateException>(() => _repository.Create(userNotFull));

            Assert.Throws<DbUpdateException>(() => _repository.Create(userIncorrectEmail));
        }
    }
}
