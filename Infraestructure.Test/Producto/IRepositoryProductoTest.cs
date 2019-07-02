using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Infraestructure.Data;
using Infraestructure.Data.Base;
using Infraestructure.Data.Repositories;
using Domain.Entities.Producto;


namespace Infraestructure.Test
{
    [TestFixture]
    class IRepositoryProductoTest
    {
        DBContextTest connection;
        Repository<Domain.Entities.Producto.Producto> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Domain.Entities.Producto.Producto>(connection);

        }

        [Test]
        public void ProductoAddTest()
        {
            
            Assert.NotNull(repository.Add(new Domain.Entities.Producto.Producto("ejemplo","ejemplo", "",1000,1200,100), true));
        }
    }
}
