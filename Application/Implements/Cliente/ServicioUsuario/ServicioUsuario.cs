using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Domain.Enum;
using Domain.Factories;
using System.Linq;


namespace Application.Implements.Cliente.ServicioUsuario
{
    public class ServicioUsuario
    {
        readonly IUnitOfWork _unitOfWork;
        public readonly IGenericRepository<Usuario> _repository;
        

        public ServicioUsuario(IUnitOfWork unitOfWork, IGenericRepository<Usuario> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        
        public ServiceResponse Create(ServicioUsuarioRequest request)
        {
            var usuario = Get(request);
            if(usuario == null)
            {
                var buildUser = BuilderFactories.Usuario(request.Username, request.Password, request.Activo, request.Rol);
                 usuario = _repository.Add(buildUser);
                if(_unitOfWork.Commit() == 1)
                {
                    return new ServiceResponse()
                    {
                        Mensaje = "Usuario Creado con exito",
                        Status = true,
                        Id = usuario.Id
                    };
                }
                else
                {
                    return new ServiceResponse() { Mensaje = "Usuario no pudo Crearse", Status = false };
                }
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Usuario Ya existe", Status = false };
            }
        }


        public Usuario Autenticar(ServicioUsuarioRequest request)
        {
            return _repository.FindBy(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefault();
        }

        public int GetId(ServicioUsuarioRequest request)
        {
            var usuario = _repository.FindBy(x => x.Username == request.Username).FirstOrDefault();
            return (usuario != null) ? usuario.Id : 0;
        }


        public Usuario Get(ServicioUsuarioRequest request)
        {
            if (request.Id != 0)
            {
                return _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
            }
            else if (request.Username != null)
            {
                return _repository.FindBy(x => x.Username == request.Username).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

    }


    public class ServicioUsuarioRequest : Usuario
    {

    }

}
