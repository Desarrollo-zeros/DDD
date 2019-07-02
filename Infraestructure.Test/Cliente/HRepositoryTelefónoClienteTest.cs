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
    class HRepositoryTelefónoClienteTest
    {
        DBContextTest connection;
        Repository<TelefónoCliente> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<TelefónoCliente>(connection);

        }

        [Test]
        public void TelefonosAddTest()
        {
            Assert.NotNull(repository.Add(new TelefónoCliente(1, 1), true));
        }

    }
}
