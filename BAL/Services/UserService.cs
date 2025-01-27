using BAL.Helpers.Convectors;
using BAL.Helpers.Gmail;
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

        private readonly IGmailHelper _gmailHelper;
        private readonly IConverterFromDbToDto<UserDB, UserDto> _converterToDto;
        private readonly IConverterFromDtoToDb<UserDB, UserDto> _converterToDb;
        public UserService(DatabaseContext databaseContext)
        {
            IEntityHelper<UserDB> userHelper = new UserHelper();
            this._crudRepository = new CrudRepository<UserDB>(databaseContext, userHelper);
            this._gmailHelper = new GmailHelper();
            this._converterToDto = new ConverterFromUserDbToUserDto();
            this._converterToDb = new ConverterFromUserDtoToUserDb();
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

        public void UpdateUser(Guid id, UserDto userDto)
        {
            var userDb = this._converterToDb.Convert(userDto);
            this._crudRepository.Update(id, userDb);
        }
    }
}
