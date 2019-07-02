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
    class NRepositoryComprobanteDePagoTest
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
        
            double[] items2 = { 1000, 200, 200 };
     
            Assert.NotNull(repository.Add(new ComprobanteDePago(0,12000,12000, 0, 12000, DateTime.Now, items2.Sum(), 1), true));
        }
    }
}
