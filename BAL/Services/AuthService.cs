using DAL.Repository;
using UserDB = DAL.Entities.User;
using UserDto = BAL.DTOs.User;
using BAL.Helpers;
using BAL.Helpers.Interfaces;
using BAL.Helpers.Gmail;
using BAL.Helpers.Convectors;
using DAL.Helpers.Interfaces;
using DAL.Helpers.EntityHelpers;
using DAL.DatabaseContextNamespace;
using BAL.Services.Interfaces;

namespace BAL.Services
{
    public class AuthService : IUserService
    {
        private CrudRepository<UserDB> _crudRepository { get; set; }

        private readonly IEncryption _encryption;
        private readonly IGmailHelper _gmailHelper;
        private readonly IConverter<UserDB, UserDto> _converter;

        public AuthService(DatabaseContext databaseContext)
        {
            IEntityHelper<UserDB> userHelper = new UserHelper();
            this._crudRepository = new CrudRepository<UserDB>(databaseContext, userHelper);
            this._encryption = new AesEncryptionHelper();
            this._gmailHelper = new GmailHelper();
            this._converter = new ConverterFromDbUserToUserDto();
        }

        public void Registration(string email, string password, string nickname)
        {
            var encryptedPassword = this._encryption.Encrypt(password);

            if (!this._gmailHelper.IsGmail(email))
            {
                throw new ArgumentException(nameof(email));
            }

            UserDto userDto = new UserDto() 
            {
                Email = email,
                Password = encryptedPassword,
                Nickname = nickname
            };

            UserDB userDB = this._converter.Convert(userDto);
            
            _crudRepository.Create(userDB);
        }

        public bool Authentication(string email, string password)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var users = _crudRepository.GetAll();

            var ifUserExist = users.Where(user => String.Equals(email, user.Email) 
            && String.Equals(password, this._encryption.Decrypt(user.Password))).Any();

            return ifUserExist;
        }
    }
}
