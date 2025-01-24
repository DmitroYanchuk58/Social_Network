using BAL.DTOs;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        public User GetUser(Guid id);

        public void DeleteUser(Guid id);

        public void UpdateUser(Guid id, User user);
    }
}
