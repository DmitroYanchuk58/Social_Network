using DAL.Entities;
using DAL.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers.EntityHelpers
{
    public class UserHelper : IEntityHelper<User>
    {
        public User CopyTo(User entityWithChanges, User entityForChange)
        {
            if (entityWithChanges.Nickname != null)
            {
                entityForChange.Nickname = entityWithChanges.Nickname;
            }
            if (entityWithChanges.Password != null)
            {
                entityForChange.Password = entityWithChanges.Password;
            }
            if (entityWithChanges.Email != null)
            {
                entityForChange.Email = entityWithChanges.Email;
            }

            return entityForChange;
        }

        public bool IsEmpty(User entity)
        {
            if (string.IsNullOrEmpty(entity.Nickname) &&
                string.IsNullOrEmpty(entity.Password) &&
                string.IsNullOrEmpty(entity.Email))
            {
                return true;
            }
            return false;
        }
    }
}
