using BAL.DTOs;
using BAL.Helpers.Convectors;
using BAL.Helpers.Interfaces;
using BAL.Services;
using DAL.DatabaseContextNamespace;
using DAL.Entities;
using DAL.Helpers.EntityHelpers;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Frozen;
using UserDb = DAL.Entities.User;
using UserDto = BAL.DTOs.User;
using UserHelper = DAL.Helpers.EntityHelpers.UserHelper;

namespace Tests.BAL_Tests
{
    public class UserServiceTests
    {
        private CrudRepository<UserDb> _crudRepository;
        private UserService _service;
        private IConverterFromDbToDto<UserDb, UserDto> _converterFromDbToDto;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().Options;
            var dbContext = new DatabaseContext(options);
            IEntityHelper<UserDb> helper = new UserHelper();
            _crudRepository = new CrudRepository<UserDb>(dbContext, helper);
            _service = new UserService(dbContext);
            this._converterFromDbToDto = new ConverterFromUserDbToUserDto();
        }

        [Test]
        public void Test_GetUser_Failure_RandomId()
        {
            Guid randomId = Guid.NewGuid();
            Assert.Throws<KeyNotFoundException>(
                () => this._service.GetUser(randomId)
            );
        }

        [Test]
        public void Test_GetUser_Success_NotThrow()
        {
            var correctId = _crudRepository.GetAll().Select(user => user.Id).First();
            Assert.DoesNotThrow(
                () => this._service.GetUser(correctId)
            );
        }

        [Test]
        public void Test_GetUser_Success_IsTheSame()
        {
            var firstUser = _crudRepository.GetAll()[0];
            var secondUser = this._service.GetUser(firstUser.Id);
            Assert.IsTrue(firstUser.Nickname == secondUser.Nickname);
        }

        [Test]
        public void Test_DeleteUser_Success()
        {
            var userForDelete = _crudRepository.GetAll().Last();
            Assert.DoesNotThrow(
                () => _service.DeleteUser(userForDelete.Id)
            );
        }

        [Test]
        public void Test_DeleteUser_Success_Count()
        {
            var countUsersBefore = _crudRepository.GetAll().Count();
            var userForDelete = _crudRepository.GetAll().Last();
            _service.DeleteUser(userForDelete.Id);
            var countUsersAfter = _crudRepository.GetAll().Count();
            Assert.That(countUsersBefore - 1 == countUsersAfter);
        }

        [Test]
        public void Test_DeleteUser_Failure()
        {
            Assert.Throws<ArgumentException>(
                () => _service.DeleteUser(Guid.NewGuid())
            );
        }

        [Test]
        public void ChangeNickname_DoesNotThrow()
        {
            var id = _crudRepository.GetAll()[27].Id;
            Assert.DoesNotThrow(
                () => _service.ChangeNickname(id, "Aragorn son of Aratorn")
            );
        }

        [Test]
        public void ChangeNickname_NicknameAfterUpdateSame()
        {
            var id = _crudRepository.GetAll()[78].Id;
            var newNickname = "Aragorn";
            _service.ChangeNickname(id, newNickname);
            var nicknameAfterUpdate = _crudRepository.GetAll()[78].Nickname;
            Assert.That(newNickname == nicknameAfterUpdate);
        }

        [Test]
        public void ChangeNickname_RandomId()
        {
            var id = Guid.NewGuid();
            Assert.Throws<ArgumentException>(
                () => _service.ChangeNickname(id, "Legolas")
            );
        }

        [Test]  
        public void ChangeNickname_NullNickname()
        {
            var id = _crudRepository.GetAll()[3].Id;
            Assert.Throws<ArgumentNullException>(
                () => _service.ChangeNickname(id, null)
            );
        }

        [Test]
        public void ChangePassword_NullPassword()
        {
            var id = _crudRepository.GetAll()[3].Id;
            Assert.Throws<ArgumentNullException>(
                () => _service.ChangePassword(id, null)
            );
        }

        [Test]
        public void ChangePassword_RandomId()
        {
            var id = Guid.NewGuid();
            Assert.Throws<ArgumentException>(
                () => _service.ChangeNickname(id, "NEwPassword")
            );
        }

        [Test]
        public void ChangePassword_DoesNotThrow()
        {
            var id = _crudRepository.GetAll()[27].Id;
            Assert.DoesNotThrow(
                () => _service.ChangeNickname(id, "Different password")
            );
        }

        [Test]
        public void ChangePassword_NicknameAfterUpdateSame()
        {
            var id = _crudRepository.GetAll()[78].Id;
            var newPassword = "Hehehehe";
            _service.ChangePassword(id, newPassword);
            var passwordAfterUpdate = _crudRepository.GetAll()[78].Password;
            passwordAfterUpdate = AesEncryptor.Decrypt(passwordAfterUpdate);
            Assert.That(newPassword == passwordAfterUpdate);
        }
    }
}
