using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implements.Factura.CompraServicio;
using Application.Implements.Producto.ProductoServicio;
using Domain.Abstracts;
using Infraestructure.Data.Base;
using Infraestructure.Data;
using NUnit.Framework;
using Application.Base;
using Application.Implements;
using Domain.Factories;
using Domain.Entities.Cliente;
using Infraestructure.Data.Repositories;
using Domain.Entities.Factura;

namespace Application.Test.Cliente.CCompraServicio
{
    [TestFixture]
    class CCompraServicioTest
    {
        IUnitOfWork _unitOfWork;
        IDbContext _db;
        Repository<CompraCliente> repositoryProductoCliente;
        Repository<Domain.Entities.Producto.Producto> repositoryProducto;
        Repository<Domain.Entities.Cliente.Cliente> repositoryCliente;
        Repository<Domain.Entities.Factura.Compra> repositoryCompra;
        Repository<ComprobanteDePago> repositoryComprobanteDePago;
        Repository<Domain.Entities.Producto.ProductoDescuento> repositoryProductoDescuento;
        Repository<Domain.Entities.Producto.Descuento> repositoryDescuento;
        Repository<Domain.Entities.Cliente.ClienteMetodoDePago> repositoryMetodoPago;



        Application.Implements.Producto.ProductoServicio.ServicioProducto ProductoServicio;
        Application.Implements.Producto.ProductoServicio.DescuentoServicio DescuentoServicio;
        Application.Implements.Producto.ProductoServicio.ProductoDescuentoServicio ProductoDescuento;
        ComprobanteDePagoServicio ComprobanteDePago;

        ProductoClienteServicio ProductoClienteServicio;
        Application.Implements.Factura.CompraServicio.CrearServicio CompraServicio;



        [SetUp()]

        public void Initializar()
        {
            _db = new DBContext();
            _unitOfWork = new UnitOfWork(_db);
            repositoryProductoCliente = new Repository<CompraCliente>(_db);
            repositoryProducto = new Repository<Domain.Entities.Producto.Producto>(_db);
            repositoryCliente = new Repository<Domain.Entities.Cliente.Cliente>(_db);
            repositoryCompra = new Repository<Compra>(_db);
            repositoryComprobanteDePago = new Repository<ComprobanteDePago>(_db);
            repositoryProductoDescuento = new Repository<Domain.Entities.Producto.ProductoDescuento>(_db);
            repositoryDescuento = new Repository<Domain.Entities.Producto.Descuento>(_db);
            repositoryMetodoPago = new Repository<ClienteMetodoDePago>(_db);


            ProductoServicio = new ServicioProducto(_unitOfWork, repositoryProducto);
            DescuentoServicio = new DescuentoServicio(_unitOfWork, repositoryDescuento);
            ProductoDescuento = new ProductoDescuentoServicio(_unitOfWork, repositoryProductoDescuento);
            CompraServicio = new Application.Implements.Factura.CompraServicio.CrearServicio(_unitOfWork,repositoryCompra);
            ComprobanteDePago = new ComprobanteDePagoServicio(_unitOfWork, repositoryComprobanteDePago);
            ProductoClienteServicio = new ProductoClienteServicio(_unitOfWork, repositoryProductoCliente);
        }


        [Test]
        public void Test()
        {
            CrearServicio productoCliente = new CrearServicio(_unitOfWork, repositoryCompra);
            var x = productoCliente.BuscarCompraPorProducto(new ServicesRequest() { Cliente_Id = 1, Compra_Id = 1 },1,repositoryProductoCliente, repositoryProducto, repositoryCliente, repositoryComprobanteDePago, repositoryProductoDescuento, repositoryDescuento, repositoryMetodoPago);
            Console.WriteLine(x.CompraClientes.FirstOrDefault().Producto.Nombre);
        }


        [Test]
        public void CrearProducto()
        {
           var x = ProductoServicio.Crear(new ProductoRequest() { CantidadProducto = 10, Descripción = "ejemplo", Imagen = "", Nombre = "algo", PrecioCompra = 1000, PrecioVenta = 1200 });
           Assert.AreEqual(x.Mensaje, "Operacion exitosa");
        }

  
        [Test]
        public void CrearCompra()
        {
            
             var x = CompraServicio.Crear(new ServicesRequest() {Cliente_Id = 1,FechaCompra = DateTime.Now });
             Assert.AreEqual(x.Mensaje, "Operacion exitosa");
            

            int id = repositoryCompra.GetAll().ToList().Last().Id;

            var y = ComprobanteDePago.Crear(new ServicesComprobanteCompraRequest() { Compra_Id = id, EstadoDePago = Domain.Enum.EstadoDePago.PAGADO, FechaDePago = DateTime.Now, MedioPago = Domain.Enum.MedioPago.EFECTIVO, Monto = 1200, SubTotal = 1200, Total = 1200, TotalDescuentoAplicados = 0.05 });
             Assert.AreEqual(y.Mensaje, "Operacion exitosa");
            

            var l = ProductoClienteServicio.Crear(new ServicesClienteProductoRequest() { Cantidad=5, Cliente_Id=1 ,Compra_Id = id, EstadoProductoCliente = Domain.Enum.EstadoClienteArticulo.NO_PAGADO,Producto_Id=1});
             Assert.AreEqual(l.Mensaje, "Operacion exitosa");
          

            var r = CompraServicio.CompletarCompra(new ServicesRequest() { Cliente_Id = 1,FechaCompra = DateTime.Now, Compra_Id = id },
                repositoryProductoCliente, 
                repositoryProducto,
                repositoryProductoDescuento,
                repositoryDescuento,
                repositoryCliente, 
                repositoryMetodoPago,
                repositoryComprobanteDePago
                );

            Assert.AreEqual(r.Mensaje, "Operacion exitosa");

        }

    }
}
