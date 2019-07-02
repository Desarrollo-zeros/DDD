using Domain.Entities.Localizacíon;
using Infraestructure.Data.Base;
using Infraestructure.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Test
{
    [TestFixture]
    class ARepositoryPaísTest
    {
        DBContextTest connection;
        Repository<País> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<País>(connection);
        }

        [Test]
        public void PaísAddTest()
        {
            Assert.NotNull(repository.Add(new País("Colombia", 0),true));
        }
    }
}
