using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cliente;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Test.Entities
{
    [TestFixture]
    class ClienteMetodoDePagoTest
    {
        //private CreditCard creditCard;
         ClienteMetodoDePago ClienteMetodoDePago ;

        [SetUp]
        public void Initialize()
        {
            ClienteMetodoDePago = new ClienteMetodoDePago(1,20000,true);
       
            //creditCard = new CreditCard(CreditCardType.Amex, "3718 892513 11442","000","carlos",new DateTime());

        }

        //error al crear meoto de pago, tarjeta de credito no cumple con el tamaño
        [Test]
        public void ValidarCreditCardNumberTestFails()
        {
            var ex = Assert.Throws<Exception>(() => new CreditCard(CreditCardType.Amex, "12345678910236", "000", "carlos", new DateTime(2019,07,29)));
            Assert.That(ex.Message, Is.EqualTo("Numero Tarjeta invalido"));
        }

        //error al crear meoto de pago, tarjeta vencidad
        [Test]
        public void ValidarCreditCardeEpirationTestFails()
        {
            var ex = Assert.Throws<Exception>(() => new CreditCard(CreditCardType.Amex, "123456789123456", "000", "carlos", new DateTime(2019, 06, 28)));
            Assert.That(ex.Message, Is.EqualTo("Tarjeta vencidad"));
        }


        [Test]
        public void AuemtarSaldoTestSuccess()
        {
            Assert.AreEqual(ClienteMetodoDePago.AumentarSaldo(1000), true);
            Assert.AreEqual(ClienteMetodoDePago.Saldo, 21000);

        }

        [Test]
        public void AuemtarSaldoTestFails()
        {
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
