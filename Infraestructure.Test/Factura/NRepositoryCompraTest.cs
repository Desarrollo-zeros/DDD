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
    class NRepositoryCompraTest
    {
        DBContextTest connection;
        Repository<Compra> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Compra>(connection);
        }

        [Test]
        public void CompraAddTest()
        {
            Assert.NotNull(repository.Add(new Compra(1, DateTime.Now, 1), true));
        }
    }
}
