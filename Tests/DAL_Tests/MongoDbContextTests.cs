using DAL.DatabaseContextNamespace;
using DAL.DbContext;
using DAL.Helpers.EntityHelpers;
using DAL.Helpers.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL_Tests
{
    public class MongoDbContextTests
    {
        private MongoDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new();
        }

        [Test]
        public void CreateKey()
        {
            var key = _dbContext.GetJwtSecretKey();
            Assert.IsNotNull(key == "Dmitro_Yanchuk_Secure_Long_Secret_Key_123!");
        }
    }
}
