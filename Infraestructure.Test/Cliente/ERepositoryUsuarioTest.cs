using Domain.Entities.Cliente;
using Domain.Enum;
using Infraestructure.Data.Repositories;
using NUnit.Framework;

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
            usuario = new Usuario("zeros", new Usuario().EncryptPassword("toor"), true, Rol.INVITADO);
        }

        /*save Usuario*/
        [Test]
        public void UsuarioAddTest()
        {
            Assert.NotNull(repository.Add(usuario, true));
        }

    }
}
