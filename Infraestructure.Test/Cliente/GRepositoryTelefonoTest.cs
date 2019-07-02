using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Infraestructure.Data;
using Infraestructure.Data.Base;
using Infraestructure.Data.Repositories;
using Domain.Entities.Cliente;

namespace Infraestructure.Test
{
    [TestFixture]
    class GRepositoryTelefonoTest
    {
        DBContextTest connection;
        Repository<Telefóno> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Telefóno>(connection);
         
        }

        [Test]
        public void TelefonoAddTest()
        {
            var telefono = new Domain.ValueObjects.Teléfono("3043541475");
            Assert.NotNull(repository.Add(new Telefóno(telefono,0), true));
        }
    }
}
