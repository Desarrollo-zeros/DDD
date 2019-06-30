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
    class FRepositoryClienteTest
    {
        DBContextTest connection;
        Repository<Domain.Entities.Cliente.Cliente> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Domain.Entities.Cliente.Cliente>(connection);
        }

        /*save Usuario*/
        [Test]
        public void ClienteAddTest()
        {
            var documento = new Domain.ValueObjects.Documento("1063969856");
            var nombre = new Domain.ValueObjects.Nombre("Carlos","Andres","Castilla","Garcia");
            var email = new Domain.ValueObjects.EmailValueObject("carloscastilla31@gmail.com");
            repository.Add(new Domain.Entities.Cliente.Cliente(documento, nombre, email,1), true);
        }
    }
}
