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
using Domain.Enum;

namespace Application.Implements.Factura.CompraServicio
{
    public class ProductoClienteServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<CompraCliente> _repository;

        public ProductoClienteServicio(IUnitOfWork unitOfWork, Repository<CompraCliente> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        
        public List<CompraCliente> BuscarProductoCLientes(int cliente_id, int producto_id)
        {
            return _repository.FindBy(x => x.Cliente_Id == cliente_id && x.Producto_Id == producto_id).ToList();
        }


        public ServiceResponse Crear(ServicesClienteProductoRequest request)
        {
            var clineteProducto = new CompraCliente(request.Cliente_Id,request.Producto_Id,request.Compra_Id,request.Cantidad,request.EstadoProductoCliente);
            _repository.Add(clineteProducto);
            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse() { Mensaje = "Operacion exitosa" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo realizar la operacion" };
            }
        }

    }


    public class ServicesClienteProductoRequest
    {
        public int Compra_Id { set; get; }
        public int Cliente_Id { set; get; }
        public int Producto_Id { set; get; }
        public int Cantidad { set; get; }
        public EstadoClienteArticulo EstadoProductoCliente { set; get; }
    }
}
