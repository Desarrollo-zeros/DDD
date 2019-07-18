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

            

            //var documento = new Domain.ValueObjects.Documento("1063969856");
            var nombre = new Domain.ValueObjects.Nombre("Carlos", "Andres", "Castilla", "Garcia");/*
            var email = new Domain.ValueObjects.EmailValueObject("carloscastilla31@gmail.com");*/
            compra.Cliente = Factories.BuilderFactories.Cliente("1063969856", nombre, "carloscastilla31@gmail.com",1); ;

            compra.Cliente.Usuario = new Usuario("0", "", true, Enum.Rol.ADMINISTRADOR)
            {
                Id = 1
            };

            var x = new List<ClienteMetodoDePago>
            {
                new ClienteMetodoDePago(1, 10000, true),
                new ClienteMetodoDePago(1, 10000, true)
            };
            compra.Cliente.ClienteMetodoDePagos = x;

            compra.Cliente.ClienteMetodoDePagos.ToList().FirstOrDefault().Cliente = compra.Cliente;

            var y = new List<CompraCliente>
            {
                new CompraCliente(1, 1, 10, Enum.EstadoClienteArticulo.NO_PAGADO)
            };
            compra.CompraClientes = y;

            compra.CompraClientes.ToList().FirstOrDefault().Producto = new Producto("algo","algo algo",null,1000,1200,10,1);

            var z = new List<ProductoDescuento>
            {
                new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO),
                new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO),
                new ProductoDescuento(1, 1, Enum.EstadoDescuento.ACTIVO)
            };

            compra.CompraClientes.ToList().FirstOrDefault().Producto.ProductoDescuentos = z;


            double descuentoAplicado =0;

            foreach(var l in compra.CompraClientes.ToList().FirstOrDefault().Producto.ProductoDescuentos){
                l.Descuento = new Descuento(Enum.TipoDescuento.FIJO, true, new DateTime(2019, 06, 29, 1, 0, 0), new DateTime(2019, 06, 30, 23, 0, 0),0.05);

                descuentoAplicado += l.Descuento.Descu;
            }

           
             compra.ComprobanteDePagos = new List<ComprobanteDePago>() { new ComprobanteDePago(Enum.EstadoDePago.EN_ESPERA, 1200, 1200, Enum.MedioPago.EFECTIVO, 12000, DateTime.Now, descuentoAplicado, 1) };

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
            compra.Cliente_Id = 1;
            double x = compra.ObtenerDescuentoPorProductoCompra( 1, 5); 
            Assert.AreEqual(900,(int) x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(1200-x), true);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 9700);
            Assert.AreEqual(compra.ComprobanteDePagos.FirstOrDefault().EstadoDePago, Enum.EstadoDePago.PAGADO);
        }

        [Test]
        public void ComprarCambioEstadoDePagoFailTest()
        {
            compra.Cliente_Id = 1;
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 5);
            Assert.AreEqual(900, (int)x);
            Assert.AreEqual(compra.DescontarTotalProductoEnSaldo(12000 - x), false);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);
            Assert.AreEqual(compra.ComprobanteDePagos.FirstOrDefault().EstadoDePago, Enum.EstadoDePago.EN_ESPERA);
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
            compra.Cliente_Id = 1;
            double x = compra.ObtenerDescuentoPorProductoCompra(1, 5);
            Assert.AreEqual((int)x, 900);
        }

        [Test]
        public void ObtenerDescuentoCompraConListaVaciaTest1()
        {
            var x = compra.CompraClientes.ToList().FirstOrDefault().Producto.ProductoDescuentos;
            x = new List<ProductoDescuento>();
            Assert.IsEmpty(x);
        }

        [Test]
        public void ObtenerDescuentoCompraConListaVaciaTest2()
        {
            compra.CompraClientes.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento = null;
            var x = compra.CompraClientes.ToList().FirstOrDefault().Producto.ProductoDescuentos.FirstOrDefault().Descuento;
            Assert.Null(x);
        }


        [Test]
        public void ObtenerDescuentoCompraExcepcionesTest()
        {
            var ex1 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra( 2, 10));
            Assert.That(ex1.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex2 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra( 1, 10));
            Assert.That(ex2.Message, Is.EqualTo("El producto Y/o el cliente no existen"));

            var ex3 = Assert.Throws<Exception>(() => compra.ObtenerDescuentoPorProductoCompra(2, 10));
            Assert.That(ex3.Message, Is.EqualTo("El producto Y/o el cliente no existen"));
        }


        [Test]
        public void ComprarArticulosSuccessTest()
        {
            
            compra.CompraClientes.FirstOrDefault().Cantidad = 5;
            Assert.AreEqual(compra.CompletarCompras(),true);
            Assert.AreEqual(compra.CompraClientes.FirstOrDefault().EstadoClienteArticulo, Enum.EstadoClienteArticulo.PAGADO);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.FirstOrDefault().Saldo, 4900);
        }


        //cuando el cliente no ha relizado compra de articulo
        //cuando no tiene saldo el cliente
        //cuando la cantidad de articulo es menor a la que va comprar
        [Test]
        public void ComprarArticulosFailsTest()
        {
            /*Assert.That(Assert.Throws<Exception>(() => compra.ComprarArticulos(5)).Message, Is.EqualTo("No hay productos para realizar la compra"));
            compra.Cliente.ClienteMetodoDePagos.First().Saldo = 0;
            Assert.That(Assert.Throws<Exception>(() => compra.ComprarArticulos(5)).Message, Is.EqualTo("No se puede completar la compra, no tiene sufienciente saldo"));
            compra.Cliente.ClienteMetodoDePagos.First().Saldo = 10000;
            compra.ComprarArticulos(compra.ProductoCliente.ToList(), 20);
            Assert.AreEqual(compra.CantidadProductoNoExistentes, 10);
            Assert.AreEqual(compra.Cliente.ClienteMetodoDePagos.First().Saldo, 10000);*/
        }


        [Test]
        public void RealizarEnvioSuccessProducto()
        {
            compra.ComprobanteDePagos.FirstOrDefault().EstadoDePago = Enum.EstadoDePago.PAGADO;
            Assert.AreEqual(compra.EnviarCompra(1), true);
            Assert.AreEqual(compra.EnviarCompra(1), true);
        }

        [Test]
        public void RealizarEnvioFailProducto()
        {
            compra.ComprobanteDePagos.FirstOrDefault().EstadoDePago = Enum.EstadoDePago.PAGADO;
            Assert.That(Assert.Throws<Exception>(() => compra.EnviarCompra(1)).Message, Is.EqualTo("No existe el producto a enviar"));
            Assert.That(Assert.Throws<Exception>(() => compra.EnviarCompra(2)).Message, Is.EqualTo("No existe Un Estado De pago"));
        }

    }
}
