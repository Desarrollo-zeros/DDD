using Domain.Entities.Localizacíon;
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
    class BRepositoryDepartamentoTest
    {
        DBContextTest connection;
        Repository<Departamento> repository;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Departamento>(connection);
        }

        [Test]
        public void DepartamentoAddTest()
        {
            repository.Add(new Departamento("Cesar",1), true);
        }
    }
}
