using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Factura;
using Domain.Entities.Producto;
using Domain.Entities.Cliente;

namespace Domain.Test.Entities
{
    [TestFixture]
    class CompraTest
    {
        private Compra compra;
        private CompraEnvio compraEnvio;
     

        [SetUp]
        public void Initialize()
        {
            compra = new Compra();
           /* var documento = new Domain.ValueObjects.Documento("1063969856");
            var nombre = new Domain.ValueObjects.Nombre("Carlos", "Andres", "Castilla", "Garcia");
            var email = new Domain.ValueObjects.EmailValueObject("carloscastilla31@gmail.com");*/
            compra.Cliente = Factories.BuilderFactories.Cliente("1063969856","carlos andres castilla garcia","carloscastilla31@gmail.com",1); ;

            var x = new List<Domain.Entities.Cliente.ClienteMetodoDePago>();
            x.Add(new Domain.Entities.Cliente.ClienteMetodoDePago(1, 10000,true));
            compra.Cliente.ClienteMetodoDePagos = x;

            var y = new List<ProductoCliente>();
            y.Add(new ProductoCliente(1, 1, 1, 10, Enum.EstadoClienteArticulo.NO_PAGADO));
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



            List<CompraEnvio> compraEnvios = new List<CompraEnvio>();
            compraEnvios.Add(new CompraEnvio(1, 1, DateTime.Now, DateTime.Now, Enum.EstadoDeEnvio.ESPERANDO_PETICION));
           
            compraEnvio = new CompraEnvio
            {
                Compra = compra
            };

            compra.CompraEnvios = compraEnvios;

            List<CompraEnvioProducto> compraEnvioProductos = new List<CompraEnvioProducto>
            {
                new CompraEnvioProducto(1, 1, 1, DateTime.Now, Enum.EstadoDeEnvioProducto.NO_ENVIADO)
            };

            compra.CompraEnvios.FirstOrDefault().CompraEnvioProductos = compraEnvioProductos;
            compra.CompraEnvios.FirstOrDefault().EstadoDeEnvio = Enum.EstadoDeEnvio.EN_VERIFICACIÓN;


           
        }


        

        //descontar el valor de la compra
        [Test]
        public void ComprarYDescontarSaldoSucessTest()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(5000),true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 5000);

        }

        //saldo a descontar no puede ser mayor o igual al saldo de la cuenta
        [Test]
        public void ComprarYDescontarSaldoFailTest()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(10000), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
        }

        [Test]
        public void ComprarCambioEstadoDePagoSuccessTest()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10); 
            Assert.AreEqual(900, x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(1200-x), true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 9700);
            Assert.AreEqual(compra.ComprobanteDePago.EstadoDePago, Enum.EstadoDePago.PAGADO);
        }

        [Test]
        public void ComprarCambioEstadoDePagoFailTest()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10);
            Assert.AreEqual(900, x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(12000 - x), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
            Assert.AreEqual(compra.ComprobanteDePago.EstadoDePago, Enum.EstadoDePago.EN_ESPERA);
        }


        //saldo a descontar debe ser mayor a 0
        [Test]
        public void ComprarConSaldoMenorOIgualACeroTest()
        {
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(0), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
        }

        
        [Test]
        public void ObtenerDescuentoCompraSuccessTest()
        {
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 1, 10);
            Assert.AreEqual(x, 900);
        }

        [Test]
        public void ObtenerDescuentoCompraConListaVaciaTest1()
        {
            var x = compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos;
            x = new List<ProductoDescuento>();
            Assert.IsEmpty(x);
        }

        [Test]
        public void ObtenerDescuentoCompraConListaVaciaTest2()
        {
            compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento = null;
            var x = compra.ProductoCliente.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento;
            Assert.Null(x);
        }


        [Test]
        public void ObtenerDescuentoCompraExcepcionesTest()
        {
            var ex1 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(1, 2, 10));
            Assert.That(ex1.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex2 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(2, 1, 10));
            Assert.That(ex2.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex3 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(2, 2, 10));
            Assert.That(ex3.Message, Is.EqualTo("El producto Y/o el cliente no existen"));
        }


        [Test]
        public void ComprarArticulosSuccessTest()
        {
            Assert.AreEqual(compra.ComprarArticulos(compra.ProductoCliente.ToList(),5),true);
            Assert.AreEqual(compra.ProductoCliente.FirstOrDefault().EstadoProductoCliente, Enum.EstadoClienteArticulo.PAGADO);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.FirstOrDefault().Saldo, 9250);
        }


        //cuando el cliente no ha relizado compra de articulo
        //cuando no tiene saldo el cliente
        //cuando la cantidad de articulo es menor a la que va comprar
        [Test]
        public void ComprarArticulosFailsTest()
        {
            Assert.That(Assert.Throws<Exception>(() => compra.ComprarArticulos(new List<ProductoCliente>(), 5)).Message, Is.EqualTo("No hay productos para realizar la compra"));
            compra.Cliente.ClienteMetodoDePagos.First().Saldo = 0;
            Assert.That(Assert.Throws<Exception>(() => compra.ComprarArticulos(compra.ProductoCliente.ToList(), 5)).Message, Is.EqualTo("No se puede completar la compra, no tiene sufienciente saldo"));
            compra.Cliente.ClienteMetodoDePagos.First().Saldo = 10000;
            compra.ComprarArticulos(compra.ProductoCliente.ToList(), 20);
            Assert.AreEqual(compra.CantidadProductoNoExistentes, 10);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
        }


        [Test]
        public void RealizarEnvioSuccessProducto()
        {
            compra.ComprobanteDePago.EstadoDePago = Enum.EstadoDePago.PAGADO;
            Assert.AreEqual(compra.EnviarCompra(1, 1), true);
            Assert.AreEqual(compra.EnviarCompra(1), true);
        }

        [Test]
        public void RealizarEnvioFailProducto()
        {
            compra.ComprobanteDePago.EstadoDePago = Enum.EstadoDePago.PAGADO;
            Assert.That(Assert.Throws<Exception>(() => compra.EnviarCompra(1, 2)).Message, Is.EqualTo("No existe el producto a enviar"));
            Assert.That(Assert.Throws<Exception>(() => compra.EnviarCompra(2, 1)).Message, Is.EqualTo("No existe Envios para esta compra"));
            Assert.That(Assert.Throws<Exception>(() => compra.EnviarCompra(2)).Message, Is.EqualTo("No existe Envios para esta compra"));
        }

    }
}
