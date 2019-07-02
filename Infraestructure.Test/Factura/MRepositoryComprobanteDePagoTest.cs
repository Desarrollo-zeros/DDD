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
    class MRepositoryComprobanteDePagoTest
    {
        DBContextTest connection;
        Repository<ComprobanteDePago> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<ComprobanteDePago>(connection);
        }

        [Test]
        public void ComprobanteDePagoAddTest()
        {
            var pago = new Domain.ValueObjects.Pago(0,12000);
            double[] items2 = { 1000, 200, 200 };
            var totalDescuentoAplicados = new Domain.ValueObjects.TotalDescuentoAplicados(items2);
            Assert.NotNull(repository.Add(new ComprobanteDePago(0,12000,12000, pago,DateTime.Now, totalDescuentoAplicados), true));
        }
    }
}
