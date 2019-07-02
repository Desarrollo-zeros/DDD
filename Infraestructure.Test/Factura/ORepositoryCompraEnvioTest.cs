using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Infraestructure.Data;
using Infraestructure.Data.Base;
using Infraestructure.Data.Repositories;
using Domain.Entities.Factura;

namespace Infraestructure.Test
{
    [TestFixture]
    class ORepositoryCompraEnvioTest
    {
        DBContextTest connection;
        Repository<CompraEnvio> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<CompraEnvio>(connection);
        }

        [Test]
        public void CompraEnvioAddTest()
        {
            Assert.NotNull(repository.Add(new CompraEnvio(1,1,DateTime.Now,DateTime.Now,0), true));
        }
    }
}
