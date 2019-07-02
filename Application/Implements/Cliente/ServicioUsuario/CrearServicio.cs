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
    public class CrearServicio : EntityService<Usuario>
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Usuario> _repository;

       public CrearServicio(IUnitOfWork unitOfWork, Repository<Usuario> repository): base(unitOfWork, repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
       }
       

        public ServiceResponse Ejecutar(ServicesRequest request)
        {
            if (_repository.FindBy(x => x.Username == request.Username).FirstOrDefault() == null)
            {
                var buildUser = BuilderFactories.Usuario(request.Username,request.Password,request.Activo);
                _repository.Add(buildUser,true);
                _unitOfWork.Commit();

                if (_repository.FindBy(x => x.Username == buildUser.Username).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Usuario Creado Con exito" };
                }
                else
                {
                    return new ServiceResponse() { Mensaje = "Usuario no pudo Crearse" };
                }
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Usuario Ya existe" };
            }
        }
    }

    public class ServicesRequest
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public bool Activo { set; get; }

        
    }

}
