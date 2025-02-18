using BAL.Helpers.Interfaces;
using UserDb = DAL.Entities.User;
using UserDto = BAL.DTOs.User;

namespace BAL.Helpers.Convectors
{
    public class ConverterFromUserDbToUserDto : IConverterFromDbToDto<UserDb, UserDto>
    {
        public UserDto Convert(UserDb userDb)
        {
            ArgumentNullException.ThrowIfNull(nameof(userDb));

            UserDto userDto = new UserDto()
            {
                Nickname = userDb.Nickname,
                Email = userDb.Email,
                Password = userDb.Password,
            };

            return userDto;
        }
    }
}
