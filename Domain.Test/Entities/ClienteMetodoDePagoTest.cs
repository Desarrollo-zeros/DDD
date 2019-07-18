using Domain.Entities.Cliente;
using Domain.Enum;
using Domain.ValueObjects;
using NUnit.Framework;
using System;

namespace Domain.Test.Entities
{
    [TestFixture]
    class ClienteMetodoDePagoTest
    {
        //private CreditCard creditCard;
        ClienteMetodoDePago ClienteMetodoDePago;

        [SetUp]
        public void Initialize()
        {
            ClienteMetodoDePago = new ClienteMetodoDePago(1, 20000, true);
            ClienteMetodoDePago.Cliente = new Cliente("1063969856", new Nombre("carlos", "andres", "", "castilla"), "carlos@ca.com", 1);
            ClienteMetodoDePago.Cliente.Usuario = new Usuario("", "", true, Rol.ADMINISTRADOR);

            //creditCard = new CreditCard(CreditCardType.Amex, "3718 892513 11442","000","carlos",new DateTime());

        }

        //error al crear meoto de pago, tarjeta de credito no cumple con el tamaño
        [Test]
        public void ValidarCreditCardNumberTestFails()
        {
            var ex = Assert.Throws<Exception>(() => new CreditCard(CreditCardType.Amex, "123456789123456", "000", "carlos", new DateTime(2019, 07, 29)));
            Assert.That(ex.Message, Is.EqualTo("Numero Tarjeta invalido"));
        }

        //error al crear meoto de pago, tarjeta vencidad
        [Test]
        public void ValidarCreditCardeEpirationTestFails()
        {
            var ex = Assert.Throws<Exception>(() => new CreditCard(CreditCardType.Amex, "5312962723161181", "000", "carlos", new DateTime(2019, 06, 28)));
            Assert.That(ex.Message, Is.EqualTo("Tarjeta vencidad"));
        }


        [Test]
        public void AuemtarSaldoTestSuccess()
        {
            Console.WriteLine(ClienteMetodoDePago.Cliente.Usuario.Rol);
            Assert.AreEqual(ClienteMetodoDePago.AumentarSaldo(1000), true);
            Assert.AreEqual(ClienteMetodoDePago.Saldo, 21000);

        }

        [Test]
        public void AuemtarSaldoTestFails()
        {
            Console.WriteLine(ClienteMetodoDePago.Cliente.Usuario.Rol);
            Assert.AreEqual(ClienteMetodoDePago.AumentarSaldo(0), false);
            Assert.AreEqual(ClienteMetodoDePago.Saldo, 20000);

        }


        [Test]
        public void DescontarSaldoTestFails()
        {
            Assert.AreEqual(ClienteMetodoDePago.DescontarSaldo(0), false);
            Assert.AreEqual(ClienteMetodoDePago.Saldo, 20000);

        }

        [Test]
        public void DescontarSaldoTestSuccess()
        {
            Assert.AreEqual(ClienteMetodoDePago.DescontarSaldo(1000), true);
            Assert.AreEqual(ClienteMetodoDePago.Saldo, 19000);

        }


    }
}
