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

namespace Application.Implements.Cliente.ServicioCliente
{
    public class CrearServicio
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Cliente.Cliente> _repositoryCliente;
        

        public CrearServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Cliente.Cliente> repository) {
            _unitOfWork = unitOfWork;
            _repositoryCliente = repository;
       }

       


        public ServiceResponse Ejecutar(ServicesRequest request)
        {
           
            if (_repositoryCliente.FindBy(x => x.Usuario_Id == request.Usuario_Id).FirstOrDefault() == null)
            {
                var buildCliente = BuilderFactories.Cliente(request.Documento,request.Nombre,request.Email,request.Usuario_Id);
                _repositoryCliente.Add(buildCliente);
                _unitOfWork.Commit();

                if (_repositoryCliente.FindBy(x => x.Documento.Numero == buildCliente.Documento.Numero).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Cliente Creado Con exito" };
                }
                else
                {
                    return new ServiceResponse() { Mensaje = "Cliente no pudo Crearse" };
                }
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Cliente Ya existe" };
            }
            
        }

        public Domain.Entities.Cliente.Cliente BuscarCliente(int cliente_id)
        {
            return _repositoryCliente.FindBy(x => x.Id == cliente_id).FirstOrDefault();
        }

        public List<Domain.Entities.Cliente.ClienteMetodoDePago> BuscarMetodoDePago(int cliente_id, Repository<Domain.Entities.Cliente.ClienteMetodoDePago> repositoryMetodoPago)
        {
            return repositoryMetodoPago.FindBy(x => x.Cliente_Id == cliente_id && x.Activo == true).ToList();
        }

    }

    public class ServicesRequest
    {
        public string Documento { set; get; }
        public string Nombre { set; get; }
        public string Email { set; get; }
        public int Usuario_Id { set; get; }

    }

}
