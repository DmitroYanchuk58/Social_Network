using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using UserDto = DAL.Entities.User;
using User = BAL.DTOs.User;
using System.Threading.Tasks;
using BAL.Helpers.Interfaces;

namespace BAL.Helpers
{
    public class UserHelper
    {
        public static UserDto ConvertUserToUserDto(User user)
        {
            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                Password = user.Password,
                Nickname = user.Nickname
            };

            return userDto;
        }
    }
}
