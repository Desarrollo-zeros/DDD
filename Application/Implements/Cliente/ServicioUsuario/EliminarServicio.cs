using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements.Cliente.ServicioUsuario
{
    public class EliminarServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Usuario> _repository;

        public EliminarServicio(IUnitOfWork unitOfWork, Repository<Usuario> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ServiceResponse Ejecutar(ServicesRequest request)
        {
            var usuario = _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
            if (usuario != null)
            {
                _repository.Delete(usuario);
                _unitOfWork.Commit();
                return new ServiceResponse() { Mensaje = "Usuario Eliminado con existos" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Usuario No existe" };
            }
        }

    }
}
