using Domain.Entities.Producto;
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
    class KRepositoryDescuentoTest
    {
        DBContextTest connection;
        Repository<Descuento> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Descuento>(connection);
        }

        [Test]
        public void DescuentoAddTest()
        {
            Assert.NotNull(repository.Add(new Descuento(0, true, new DateTime(2019, 06, 29, 1, 0, 0), new DateTime(2019, 06, 30, 23, 0, 0), 0.05), true));
        }

    }
}
