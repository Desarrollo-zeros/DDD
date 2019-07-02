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
    class CRepositoryMunicipioTest
    {
        DBContextTest connection;

        Repository<Municipio> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Municipio>(connection);
        }

        [Test]
        public void MunicipioAddTest()
        {
            Assert.NotNull(repository.Add(new Municipio("Valledupar",1), true));
        }
    }
}
