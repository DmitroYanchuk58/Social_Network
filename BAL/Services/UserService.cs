using BAL.DTOs;
using DAL.Entities;
using DAL.Repository;
using UserDB = DAL.Entities.User;
using User = BAL.DTOs.User;
using BAL.Helpers;
using BAL.Helpers.Interfaces;
using BAL.Helpers.Gmail;

namespace BAL.Services
{
    public class UserService
    {
        private CrudRepository<UserDB> _crudRepository { get; set; }

        private readonly IEncryption _encryption;
        private readonly IGmailHelper _gmailHelper;

        public UserService(CrudRepository<UserDB> crudRepository)
        {
            this._crudRepository = crudRepository;
            this._encryption = new AesEncryptionHelper();
            this._gmailHelper = new GmailHelper();
        }

        public void Registration(string email, string password, string nickname)
        {
            var encryptedPassword = this._encryption.Encrypt(password);

            if (!this._gmailHelper.IsGmail(email))
            {
                throw new ArgumentException(nameof(email));
            }

            User user = new User() 
            {
                Email = email,
                Password = encryptedPassword,
                Nickname = nickname
            };

            UserDB userDB = UserHelper.ConvertUserToUserDto(user);
            
            _crudRepository.Create(userDB);
        }

        public bool Authentication(string email, string password)
        {
            var users = _crudRepository.GetAll();

            var ifUserExist = users.Where(user => user.Email == email 
            && user.Password == this._encryption.Decrypt(password)).Any();

            return ifUserExist;
        }
    }
}
