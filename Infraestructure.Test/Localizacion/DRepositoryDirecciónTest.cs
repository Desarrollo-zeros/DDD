using Domain.Entities.Localizacíon;
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
    class DRepositoryDirecciónTest
    {
        DBContextTest connection;

        Repository<Dirección> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Dirección>(connection);
        }

        [Test]
        public void DirecciónAddTest()
        {
            Assert.NotNull(repository.Add(new Dirección("San jorge","calle 21A N° 5b-61","ABC123",1), true));
        }
    }
}
