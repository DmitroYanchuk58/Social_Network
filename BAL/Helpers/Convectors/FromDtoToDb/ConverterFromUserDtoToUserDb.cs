using BAL.Helpers.Interfaces;
using UserDb = DAL.Entities.User;
using UserDto = BAL.DTOs.User;

namespace BAL.Helpers.Convectors
{
    public class ConverterFromUserDtoToUserDb : IConverterFromDtoToDb<UserDb, UserDto>
    {
        public UserDb Convert(UserDto userDto)
        {
            if (userDto == null)
            {
                ArgumentNullException.ThrowIfNull(nameof(userDto));
            }

            UserDb userDb = new UserDb()
            {
                Nickname = userDto.Nickname,
                Email = userDto.Email,
                Password = userDto.Password,
            };

            return userDb;
        }
    }
}
