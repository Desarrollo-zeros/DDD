using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Factura;
using Domain.Entities.Producto;

namespace Domain.Test.Entities
{
    [TestFixture]
    class CompraTest
    {
        private Compra compra;
     

        [SetUp]
        public void Initialize()
        {
            compra = new Compra();
            var documento = new Domain.ValueObjects.Documento("1063969856");
            var nombre = new Domain.ValueObjects.Nombre("Carlos", "Andres", "Castilla", "Garcia");
            var email = new Domain.ValueObjects.EmailValueObject("carloscastilla31@gmail.com");
            compra.Cliente = new Domain.Entities.Cliente.Cliente(documento,nombre,email,1);
            var x = new List<Domain.Entities.Cliente.ClienteMetodoDePago>();
            x.Add(new Domain.Entities.Cliente.ClienteMetodoDePago(1, 10000,true));
            compra.Cliente.ClienteMetodoDePagos = x;

            var y = new List<ProductoCliente>();
            y.Add(new ProductoCliente(1, 1, 1, 10,false));
            compra.ProductoCliente = y;

            compra.ProductoCliente.ToList().FirstOrDefault().Producto = new Producto("algo","algo algo",null,1000,1200,10);

            var z = new List<ProductoDescuento>();
            z.Add(new ProductoDescuento(1,1,Enum.EstadoDescuento.ACTIVO));
            z.Add(new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO));
            z.Add(new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO));

            compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos = z;


            int i = 0;

            double[] descuentoAplicado = { };

            foreach(var l in compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos){
                l.Descuento = new Descuento(Enum.TipoDescuento.FIJO, true, new DateTime(2019, 06, 29, 1, 0, 0), new DateTime(2019, 06, 30, 23, 0, 0), 0.02+0.005*i);
                i++;
                descuentoAplicado.ToList().Add(l.Descuento.Descu);
            }

            ValueObjects.Pago pago = new ValueObjects.Pago(Enum.MedioPago.EFECTIVO,12000);
            ValueObjects.TotalDescuentoAplicados totalDescuentoAplicados = new ValueObjects.TotalDescuentoAplicados(descuentoAplicado);
            compra.ComprobanteDePago = new ComprobanteDePago(Enum.EstadoDePago.EN_ESPERA,12000,12000, pago,DateTime.Now, totalDescuentoAplicados);

        }


        public void Initialize(double valor)
        {
            compra = new Compra();
            var documento = new Domain.ValueObjects.Documento("1063969856");
            var nombre = new Domain.ValueObjects.Nombre("Carlos", "Andres", "Castilla", "Garcia");
            var email = new Domain.ValueObjects.EmailValueObject("carloscastilla31@gmail.com");
            compra.Cliente = new Domain.Entities.Cliente.Cliente(documento, nombre, email, 1);
            var x = new List<Domain.Entities.Cliente.ClienteMetodoDePago>();
            x.Add(new Domain.Entities.Cliente.ClienteMetodoDePago(1, 10000, true));
            compra.Cliente.ClienteMetodoDePagos = x;

            var y = new List<ProductoCliente>();
            y.Add(new ProductoCliente(1, 1, 1, 10, false));
            compra.ProductoCliente = y;

            compra.ProductoCliente.ToList().FirstOrDefault().Producto = new Producto("algo", "algo algo", null, 1000, valor, 10);

            var z = new List<ProductoDescuento>();
            z.Add(new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO));
            z.Add(new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO));
            z.Add(new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO));

            compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos = z;


            int i = 0;

            double[] descuentoAplicado = { };

            foreach (var l in compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos)
            {
                l.Descuento = new Descuento(Enum.TipoDescuento.FIJO, true, new DateTime(2019, 06, 29, 1, 0, 0), new DateTime(2019, 06, 30, 23, 0, 0), 0.02 + 0.005 * i);
                i++;
                descuentoAplicado.ToList().Add(l.Descuento.Descu);
            }

            ValueObjects.Pago pago = new ValueObjects.Pago(Enum.MedioPago.EFECTIVO, 12000);
            ValueObjects.TotalDescuentoAplicados totalDescuentoAplicados = new ValueObjects.TotalDescuentoAplicados(descuentoAplicado);
            compra.ComprobanteDePago = new ComprobanteDePago(Enum.EstadoDePago.EN_ESPERA, 12000, 12000, pago, DateTime.Now, totalDescuentoAplicados);

        }

        //descontar el valor de la compra
        [Test]
        public void ComprarTestSuccess()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(5000),true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 5000);

        }

        //saldo a descontar no puede ser mayor o igual al saldo de la cuenta
        [Test]
        public void ComprarTestFails1()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(10000), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
        }

        [Test]
        public void ComprarCambioEstadoDePagoTestSuccess()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10); 
            Assert.AreEqual(900, x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(1200-x), true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 9700);
            Assert.AreEqual(compra.ComprobanteDePago.EstadoDePago, Enum.EstadoDePago.PAGADO);
        }

        [Test]
        public void ComprarCambioEstadoDePagoTestFails()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10);
            Assert.AreEqual(900, x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(12000 - x), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
            Assert.AreEqual(compra.ComprobanteDePago.EstadoDePago, Enum.EstadoDePago.EN_ESPERA);
        }


        //saldo a descontar debe ser mayor a 0
        [Test]
        public void ComprarTestFails2()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(0), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
        }

        
        [Test]
        public void ObtenerDescuentoCompraTestSuccess()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10);
            Assert.AreEqual(x, 900);
        }

        [Test]
        public void ObtenerDescuentoCompraTestFailsEmpty()
        {
            var x = compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos;
            x = new List<ProductoDescuento>();
            Assert.IsEmpty(x);
        }

        [Test]
        public void ObtenerDescuentoCompraTestFailsNulle()
        {
            compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento = null;
            var x = compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento;
            Assert.Null(x);
        }


        [Test]
        public void ObtenerDescuentoCompraTestFails()
        {
            var ex1 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(1, 2, 10));
            Assert.That(ex1.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex2 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(2, 1, 10));
            Assert.That(ex2.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex3 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(2, 2, 10));
            Assert.That(ex3.Message, Is.EqualTo("El producto Y/o el cliente no existen"));
        }


        [Test]
        public void ComprarArticulosTestSuccess()
        {
            //1200, 0.075, 5 = 450 descuento
            //1200 - 450 = 750
            //10000 - 750 = 
            Assert.AreEqual(compra.ComprarArticulos(compra.ProductoCliente.ToList(),5),true);
            Assert.AreEqual(compra.ProductoCliente.FirstOrDefault().EstadoProductoCliente, true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.FirstOrDefault().Saldo, 9250);
        }


        [Test]
        public void ComprarArticulosTestFails()
        {

       
            var ex1 = Assert.Throws<Exception>(() => compra.ComprarArticulos(new List<ProductoCliente>(), 5));
            Assert.That(ex1.Message, Is.EqualTo("No hay productos para realizar la compra"));
            Initialize(120000);
            var ex2 = Assert.Throws<Exception>(() => compra.ComprarArticulos(compra.ProductoCliente.ToList(), 5));
            Assert.That(ex2.Message, Is.EqualTo("No se puede completar la compra, no tiene sufienciente saldo"));
            Initialize();
    
            var ex3 = Assert.Throws<Exception>(() => compra.ComprarArticulos(compra.ProductoCliente.ToList(), 20));
            Assert.That(ex3.Message, Is.EqualTo("No se puede completar la compra, no tiene sufienciente saldo"));


            /*Assert.AreEqual(compra.ComprarArticulos(new List<ProductoCliente>(), 5), false);
            Assert.AreEqual(compra.ProductoCliente.FirstOrDefault().EstadoProductoCliente, false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.FirstOrDefault().Saldo, 10000);*/


        }

    }
}
