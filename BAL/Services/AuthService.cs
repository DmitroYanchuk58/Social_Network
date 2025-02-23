using DAL.Repository;
using UserDB = DAL.Entities.User;
using UserDto = BAL.DTOs.User;
using BAL.Helpers.Interfaces;
using BAL.Helpers.Gmail;
using BAL.Helpers.Convectors;
using DAL.Helpers.Interfaces;
using DAL.Helpers.EntityHelpers;
using DAL.DatabaseContextNamespace;
using BAL.Services.Interfaces;

namespace BAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly CrudRepository<UserDB> _crudRepository;
        private readonly ConverterFromUserDtoToUserDb _converter;

        public AuthService(DatabaseContext databaseContext)
        {
            IEntityHelper<UserDB> userHelper = new UserHelper();
            this._crudRepository = new CrudRepository<UserDB>(databaseContext, userHelper);
            this._converter = new ConverterFromUserDtoToUserDb();
        }

        public void Registration(string email, string password, string nickname)
        {
            ArgumentException.ThrowIfNullOrEmpty(email);
            ArgumentException.ThrowIfNullOrEmpty(password);
            ArgumentException.ThrowIfNullOrEmpty(nickname);

            if (!IsGmailChecker.IsGmail(email))
            {
                throw new ArgumentException("Email can't be null",nameof(email));
            }

            var (encryptedPassword, iv) = AesEncryptor.Encrypt(password);
            IVKeyService.CreateIVKey(email, iv);

            UserDto userDto = new() 
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

            try
            {
                var users = _crudRepository.GetAll();

                var iv = IVKeyService.GetIVKKey(email);

                var ifUserExist = users.Any(user => String.Equals(email, user.Email)
                && String.Equals(password, AesEncryptor.Decrypt(user.Password, iv)));

                return ifUserExist;
            }
            catch
            {
                return false;
            }
        }
    }
}
