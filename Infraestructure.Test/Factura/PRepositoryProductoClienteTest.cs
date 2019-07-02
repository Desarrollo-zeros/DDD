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
    class PRepositoryProductoClienteTest
    {
        DBContextTest connection;
        Repository<ProductoCliente> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<ProductoCliente>(connection);
        }

        [Test]
        public void CompraEnvioAddTest()
        {
            Assert.NotNull(repository.Add(new ProductoCliente(1,1,1,10,Domain.Enum.EstadoClienteArticulo.NO_PAGADO), true));
        }
    }
}
