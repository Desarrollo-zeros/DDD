using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Domain.Entities.Cliente;
using Domain.Factories;
using Domain.Base;

namespace Domain.Test.Entities
{
    [TestFixture]
    class UsuarioTest
    {
        private Usuario usuario;

        [SetUp]
        public void Initialize()
        {
            usuario = Factories.BuilderFactories.Usuario("zeros","toor",true);
        }

        //usuario y contraseña validos
        [Test]
        public void AutentificateTestSuccess()
        {
            var auth = usuario.Authenticate("zeros", usuario.EncryptPassword("toor"));
            Assert.AreEqual(auth, true);

        }

        //usuarios y contraseña invalidos
        [Test]
        public void AutentificateTestFails()
        {
            var auth = usuario.Authenticate("zeros", "123");
            Assert.AreEqual(auth, false);
        }

    }
}
