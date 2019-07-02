using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using Domain.Factories;
using Domain.Entities.Cliente;
using Domain.Abstracts;
using Infraestructure.Data.Repositories;
using Application.Base;

namespace Application.Implements.Cliente.ServicioUsuario
{
    public class ModificarServicio 
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Usuario> _repository;

        public ModificarServicio(IUnitOfWork unitOfWork, Repository<Usuario> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ServiceResponse Ejecutar(ServicesRequest request)
        {
            var usuario = _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
            if (usuario != null)
            {
                var u = BuilderFactories.Usuario(request.Username, request.Password, request.Activo);
                usuario.Username = u.Username;
                usuario.Password = u.Password;
                usuario.Activo = u.Activo;

                _repository.Edit(usuario);
                _unitOfWork.Commit();
                return new ServiceResponse() { Mensaje = "Usuario Modificado con exito" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Usuario No existe" };
            }
        }
    }
}
