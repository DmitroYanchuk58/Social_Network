namespace BAL.Services.Interfaces
{
    public interface IAuthService
    {
        public void Registration(string email, string password, string nickname);

        public bool Authentication(string email, string password);
    }
}
