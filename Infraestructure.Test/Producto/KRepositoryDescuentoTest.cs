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
            repository.Add(new Descuento(0,true,DateTime.Now,new DateTime(2019,06,30)), true);
        }

    }
}
