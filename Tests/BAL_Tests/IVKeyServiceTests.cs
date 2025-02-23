using BAL.Helpers.Interfaces;
using BAL.Services;
using System.Security.Cryptography;

namespace Tests.BAL_Tests
{
    public class IVKeyServiceTests
    {
        [Test]
        public void KeysAreSame()
        {
            var aes = Aes.Create();
            aes.GenerateIV();
            var iv = aes.IV;
            IVKeyService.CreateIVKey("idkWhatPasswordHonestly@gmail.com", iv);

            var ivFromDatabase = IVKeyService.GetIVKKey("idkWhatPasswordHonestly@gmail.com");

            Assert.That(iv, Is.EqualTo(ivFromDatabase));
        }
    }
}
