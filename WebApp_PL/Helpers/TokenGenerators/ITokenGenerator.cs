namespace WebApp_PL.Helpers.TokenGenerators
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username);
    }
}
