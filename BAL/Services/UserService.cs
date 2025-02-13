using BAL.Helpers.Convectors;
using BAL.Helpers.Interfaces;
using BAL.Services.Interfaces;
using DAL.DatabaseContextNamespace;
using DAL.Helpers.EntityHelpers;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using UserDB = DAL.Entities.User;
using UserDto = BAL.DTOs.User;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private CrudRepository<UserDB> _crudRepository { get; set; }

        private readonly IConverterFromDbToDto<UserDB, UserDto> _converterToDto;

        public UserService(DatabaseContext databaseContext)
        {
            IEntityHelper<UserDB> userHelper = new UserHelper();
            this._crudRepository = new CrudRepository<UserDB>(databaseContext, userHelper);
            this._converterToDto = new ConverterFromUserDbToUserDto();
        }

        public UserDto GetUser(Guid id)
        {
            UserDB userDb;
            try
            {
                userDb = this._crudRepository.Get(id);
            }
            catch
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            var userDto = this._converterToDto.Convert(userDb);
            return userDto;
        }

        public void DeleteUser(Guid id)
        {
            this._crudRepository.Delete(id);
        }

        public void ChangeNickname(Guid id, string newNickname)
        {
            if (string.IsNullOrWhiteSpace(newNickname))
            {
                throw new ArgumentNullException(nameof(newNickname));
            }

            var user = new UserDB()
            {
                Nickname = newNickname,
                Password = null,
                Email = null
            };

            _crudRepository.Update(id, user);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentNullException(nameof(newPassword));
            }

            newPassword = AesEncryptor.Encrypt(newPassword);

            var user = new UserDB()
            {
                Nickname = null,
                Password = newPassword,
                Email = null
            };

            _crudRepository.Update(id, user);
        }
    }
}
