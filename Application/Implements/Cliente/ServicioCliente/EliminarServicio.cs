using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements.Cliente.ServicioCliente
{
    public class EliminarServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Cliente.Cliente> _repository;

        public EliminarServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Cliente.Cliente> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ServiceResponse Ejecutar(ServicesRequest request)
        {
            var usuario = _repository.FindBy(x => x.Id == request.Usuario_Id).FirstOrDefault();
            if (usuario != null)
            {
                _repository.Delete(usuario);
                _unitOfWork.Commit();
                return new ServiceResponse() { Mensaje = "Cliente Eliminado con existos" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Cliente No existe" };
            }
        }

    }
}
