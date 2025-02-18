using BAL.Services;

namespace Tests.BAL_Tests
{
    public class JwtKeyServiceTests
    {
        private JwtKeyService service;

        [SetUp]
        public void Setup()
        {
            service = new JwtKeyService();
        }

        [Test]
        public void IsJwtKeyRight()
        {
            var key = service.GetJwtSecretKey();
            Assert.That(key, Is.EqualTo("Dmitro_Yanchuk_Secure_Long_Secret_Key_123!"));
        }
    }
}
