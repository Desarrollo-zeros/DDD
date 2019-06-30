﻿using System;
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
    class ERepositoryUsuarioTest
    {
        DBContextTest connection;
        Repository<Usuario> repository;
        Usuario usuario;

        [SetUp]
        public void Initialize()
        {
            connection = new DBContextTest();
            repository = new Repository<Usuario>(connection);
            usuario = new Usuario("zeros", new Usuario().EncryptPassword("toor"), true);
        }

        /*save Usuario*/
        [Test]
        public void UsuarioAddTest()
        {
            repository.Add(usuario,true);
        }

    }
}