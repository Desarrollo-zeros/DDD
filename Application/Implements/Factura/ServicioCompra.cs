using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Factura;
using Domain.Factories;
using System;

namespace Application.Implements.Factura
{
    public class ServicioCompra : Base.EntityService<Compra>
    {
        readonly IUnitOfWork _unitOfWork;

        public ServicioCompra(IUnitOfWork unitOfWork, IGenericRepository<Compra> repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResponse Create(ServicioCompraRequest request)
        {
            if (request == null)
            {
                return new ServiceResponse
                {
                    Mensaje = "Compra no debe estar vacia",
                    Status = false
                };
            }

            var compra = base.Create(BuilderFactories.Compra(request.Cliente_Id, request.FechaCompra));
            if (compra == null)
            {
                return new ServiceResponse
                {
                    Mensaje = "Compra no pudo crearse",
                    Status = false
                };
            }

            return new ServiceResponse
            {
                Id = compra.Id,
                Mensaje = "Compra Creada con exito",
                Status = true
            };

        }


        public Compra CompletarCompra(ServicioCompraRequest request)
        {
            if (request == null)
            {
                throw new Exception("Compra no debe estar vacia");
            }

            if (request.CompraClientes == null)
            {
                throw new Exception("Compra Clientes no debe estar vacia");
            }

            if (request.Cliente == null)
            {
                throw new Exception("Clientes no debe estar vacia");
            }

            if (request.Cliente.ClienteMetodoDePagos == null)
            {
                throw new Exception("ClienteMetodoDePagos no debe estar vacia");
            }

            if (request.Cliente.Usuario == null)
            {
                throw new Exception("ClienteMetodoDePagos no debe estar vacia");
            }
            request.CompletarCompras();
            return request;
        }
    }

    public class ServicioCompraRequest : Compra
    {

    }

}
