using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Domain.Factories;
using Domain.Entities.Cliente;

namespace Domain.Test.Entities
{
    [TestFixture]
    class UsuarioTest
    {
        private Usuario usuario = FactoryPattern<Usuario>.CreateInstance();

        [SetUp]
        public void Initialize()
        {
            usuario.Username = "zeros";
            usuario.Password = usuario.EncryptPassword("toor");
            usuario.Active = true;
        }

        //usuario y contraseña validos
        [Test]
        public void AutentificateTestSuccess()
        {
            var auth = usuario.Authenticate("zeros", "toor");
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
