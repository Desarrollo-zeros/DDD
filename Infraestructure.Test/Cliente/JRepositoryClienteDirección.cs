using Infraestructure.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cliente;

namespace Infraestructure.Test
{
    [TestFixture]
    class JRepositoryClienteDirección
    {

        DBContextTest connection;
        Repository<ClienteDireccíon> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<ClienteDireccíon>(connection);
        }

        [Test]
        public void ClienteDirecciónAddTest()
        {
            Assert.NotNull(repository.Add(new ClienteDireccíon(1,1), true));
        }
    }
}
