using BAL.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDb = DAL.Entities.User;
using UserDto = BAL.DTOs.User;

namespace BAL.Helpers.Convectors
{
    public class ConverterFromDbUserToUserDto : IConverter<UserDb, UserDto>
    {
        public UserDb Convert(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
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
