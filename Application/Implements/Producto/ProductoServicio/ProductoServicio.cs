using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using Domain.Factories;

using Domain.Abstracts;
using Infraestructure.Data.Repositories;
using Application.Base;
using Domain.Entities.Producto;
using Domain.ValueObjects;
using Domain.Enum;

namespace Application.Implements.Producto.ProductoServicio
{
    public class ProductoServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Producto.Producto> _repository;
    

        public ProductoServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Producto.Producto> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        

        public ServiceResponse Crear(ProductoRequest request)
        {
            var producto = new Domain.Entities.Producto.Producto(request.Nombre,request.Descripción,request.Imagen,request.PrecioCompra,request.PrecioVenta,request.CantidadProducto,1);
            _repository.Add(producto);
            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse() { Mensaje = "Operacion exitosa" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo realizar la operacion" };
            }
        }


        public Domain.Entities.Producto.Producto BuscarProducto(int producto_id)
        {
            return _repository.FindBy(x => x.Id == producto_id).FirstOrDefault();
        }

        public List<Domain.Entities.Producto.ProductoDescuento> BuscarProductoDescuentos(int producto_id, Repository<Domain.Entities.Producto.ProductoDescuento> repositoryProductoDescuento)
        {
            return repositoryProductoDescuento.FindBy(x => x.Producto_Id == producto_id).ToList();
        }

        public Domain.Entities.Producto.Descuento BuscarDescuento(int descuento_id, Repository<Domain.Entities.Producto.Descuento> repositoryDescuento)
        {
            return repositoryDescuento.FindBy(x => x.Id == descuento_id).FirstOrDefault();
        }
    }


    public class ProductoRequest
    {
        public string Nombre { set; get; }
        public string Descripción { set; get; }
        public string Imagen { set; get; }
        public double PrecioCompra { set; get; }
        public double PrecioVenta { set; get; }
        public int CantidadProducto { set; get; }
    }



    public class DescuentoServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Producto.Descuento> _repository;


        public DescuentoServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Producto.Descuento> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }



        public ServiceResponse Crear(DescuentoRequest request)
        {
            var descuento = new Domain.Entities.Producto.Descuento(request.TipoDescuento,request.Acomulable,request.FechaYHoraInicio,request.FechaYHoraTerminación,request.Descu);
            _repository.Add(descuento);
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

    public class DescuentoRequest
    {
        public TipoDescuento TipoDescuento { set; get; }
        public bool Acomulable { set; get; }
        public DateTime FechaYHoraInicio { set; get; }
        public DateTime FechaYHoraTerminación { set; get; }
        public double Descu { set; get; }

    }


    public class ProductoDescuentoServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Producto.ProductoDescuento> _repository;


        public ProductoDescuentoServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Producto.ProductoDescuento> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public ServiceResponse Crear(ProductoDescuentoRequest request)
        {
            var descuento = new Domain.Entities.Producto.ProductoDescuento(request.Producto_Id,request.Descuento_Id,request.EstadoDescuento);
            _repository.Add(descuento);
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

    public class ProductoDescuentoRequest
    {
        public int Producto_Id { set; get; }
        public int Descuento_Id { set; get; }
        public EstadoDescuento EstadoDescuento { set; get; }
    }

}
