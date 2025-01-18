namespace BAL.Helpers.Interfaces
{
    public interface IEncryption
    {
        public string Encrypt(string plaintext);

        public string Decrypt(string ciphertext);
    }
}
