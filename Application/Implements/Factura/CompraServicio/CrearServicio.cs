using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using Domain.Factories;
using Domain.Entities.Factura;
using Domain.Abstracts;
using Infraestructure.Data.Repositories;
using Application.Base;
using Application.Implements.Producto.ProductoServicio;
using Application.Implements.Cliente;
using Domain.Enum;

namespace Application.Implements.Factura.CompraServicio
{
    public class CrearServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Compra> _repositoryCompra;



        public CrearServicio(IUnitOfWork unitOfWork, Repository<Compra> repository)
        {
            _unitOfWork = unitOfWork;
            _repositoryCompra = repository;

        }



        public ServiceResponse Crear(ServicesRequest request)
        {
            var compraCliente = new Compra(request.Cliente_Id, request.FechaCompra);
            _repositoryCompra.Add(compraCliente);
            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse() { Mensaje = "Operacion exitosa" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo realizar la compra" };
            }
        }

        public ServiceResponse CompletarCompra(ServicesRequest request, Repository<CompraCliente> productoClientes, Repository<Domain.Entities.Producto.Producto> productos, Repository<Domain.Entities.Producto.ProductoDescuento> productoDescuentos, Repository<Domain.Entities.Producto.Descuento> descuentos, Repository< Domain.Entities.Cliente.Cliente> clientes, Repository<Domain.Entities.Cliente.ClienteMetodoDePago> clienteMetodoDePagos, Repository<Domain.Entities.Factura.ComprobanteDePago> comprobanteDePagos)
        {
           
            var compra = _repositoryCompra.FindBy(z => z.Id == request.Compra_Id).FirstOrDefault();
            compra.Cliente = clientes.FindBy(c => c.Id == compra.Cliente_Id).FirstOrDefault();
            compra.Cliente.ClienteMetodoDePagos = clienteMetodoDePagos.FindBy(c => c.Cliente_Id == compra.Cliente_Id && c.Activo == true).ToList();
            compra.ComprobanteDePagos = comprobanteDePagos.FindBy(c => c.Compra_Id == request.Compra_Id).ToList();
            compra.CompraClientes = productoClientes.FindBy(m => m.Compra_Id == request.Compra_Id && m.EstadoProductoCliente == EstadoClienteArticulo.NO_PAGADO).ToList();
            compra.CompraClientes.ToList().ForEach(x =>
            {
                x.Producto = productos.FindBy(f=>f.Id == x.Producto_Id).FirstOrDefault();
                x.Producto.ProductoDescuentos = productoDescuentos.GetAll().ToList();

                x.Producto.ProductoDescuentos.ToList().ForEach(y =>
                {
                    y.Descuento = descuentos.FindBy(r => r.Id == y.Descuento_Id).FirstOrDefault();
                });

            });
            if (compra.ComprarArticulos())
            {
                _repositoryCompra.Edit(compra);
                compra.CompraClientes.ToList().ForEach(x =>
                {
                    productoClientes.Edit(x);
                });

                clienteMetodoDePagos.Edit(compra.Cliente.ClienteMetodoDePagos.FirstOrDefault());
                comprobanteDePagos.Edit(compra.ComprobanteDePagos.FirstOrDefault());
                _unitOfWork.Commit();

                return new ServiceResponse() { Mensaje = "Operacion exitosa" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo realizar la Operacion" };
            }
        }

        public Compra BuscarCompraPorProducto(ServicesRequest request, int producto_id, Repository<CompraCliente> repositoryProductoCliente, Repository<Domain.Entities.Producto.Producto> repositoryProducto, Repository<Domain.Entities.Cliente.Cliente> repositoryCliente, Repository<ComprobanteDePago> repositoryComprobanteDePago, Repository<Domain.Entities.Producto.ProductoDescuento> repositoryProductoDescuento, Repository<Domain.Entities.Producto.Descuento> repositoryDescuento, Repository<Domain.Entities.Cliente.ClienteMetodoDePago> repositoryMetodoPago)
        {
            /*ProductoClienteServicio productoClienteServicio = new ProductoClienteServicio(_unitOfWork, repositoryProductoCliente);
            ProductoServicio producto = new ProductoServicio(_unitOfWork, repositoryProducto);
            var cliente = new Cliente.ServicioCliente(_unitOfWork, repositoryCliente);

            var compra = _repositoryCompra.FindBy(z => z.Id == request.Compra_Id).FirstOrDefault();
            compra.ComprobanteDePagos = repositoryComprobanteDePago.FindBy(v => v.Compra_Id == compra.Id).ToList();
            compra.Cliente = cliente.BuscarCliente(compra.Cliente_Id); ;
            compra.ProductoCliente = productoClienteServicio.BuscarProductoCLientes(request.Cliente_Id, producto_id);
            compra.ProductoCliente.ToList().ForEach(x =>
            {
                x.Producto = producto.BuscarProducto(producto_id);
                x.Producto.ProductoDescuentos = producto.BuscarProductoDescuentos(producto_id, repositoryProductoDescuento);
                x.Producto.ProductoDescuentos.ToList().ForEach(y =>
                {
                    y.Descuento = producto.BuscarDescuento(y.Descuento_Id, repositoryDescuento);
                });

            });*/
            return null;
        }

    }



    public class ComprobanteDePagoServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Factura.ComprobanteDePago> _repositoryCompra;



        public ComprobanteDePagoServicio(IUnitOfWork unitOfWork, Repository<ComprobanteDePago> repository)
        {
            _unitOfWork = unitOfWork;
            _repositoryCompra = repository;

        }

        public ServiceResponse Crear(ServicesComprobanteCompraRequest request)
        {
            var comprobanteDePago = new ComprobanteDePago(request.EstadoDePago,request.Total,request.SubTotal, request.MedioPago,request.Monto,request.FechaDePago,request.TotalDescuentoAplicados,request.Compra_Id);
            _repositoryCompra.Add(comprobanteDePago);
            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse() { Mensaje = "Operacion exitosa" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo realizar la compra" };
            }
        }
    }




    public class ServicesRequest
    {
            public int Compra_Id { set; get; } 
            public int Cliente_Id { set; get; }
            public DateTime FechaCompra { set; get; }   
    }

    public class ServicesComprobanteCompraRequest
    {
        public int Compra_Id { set; get; }
        public Domain.Enum.EstadoDePago EstadoDePago { set; get; }
        public Domain.Enum.MedioPago MedioPago { set; get; }
        public double Total { set; get; }
        public double SubTotal { set; get; }
        public double Monto { get; set; }
        public DateTime FechaDePago { set; get; }
        public double TotalDescuentoAplicados { set; get; }
    }
    

}
