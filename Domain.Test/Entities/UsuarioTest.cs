using Domain.Entities.Cliente;
using NUnit.Framework;

namespace Domain.Test.Entities
{
    [TestFixture]
    class UsuarioTest
    {
        private Usuario usuario;

        [SetUp]
        public void Initialize()
        {
            usuario = Factories.BuilderFactories.Usuario("zeros", "toor", true, Enum.Rol.INVITADO);
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
