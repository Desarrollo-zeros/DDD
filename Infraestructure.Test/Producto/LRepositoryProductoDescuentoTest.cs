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
    class LRepositoryProductoDescuentoTest
    {
        DBContextTest connection;
        Repository<ProductoDescuento> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<ProductoDescuento>(connection);
        }

        [Test]
        public void ProductoDescuentoAddTest()
        {
            Assert.NotNull(repository.Add(new ProductoDescuento(1,1,Domain.Enum.EstadoDescuento.ACTIVO), true));
        }
    }
}
