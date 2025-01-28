using BAL.DTOs;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        public User GetUser(Guid id);

        public void DeleteUser(Guid id);

        public void ChangePassword(Guid id, string newPassword);

        public void ChangeNickname(Guid id, string newNickname);
    }
}
