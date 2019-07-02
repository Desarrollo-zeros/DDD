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
    public class ModificarServicio 
    {
        readonly IUnitOfWork _unitOfWork;
        Repository<Domain.Entities.Cliente.Cliente> _repository;

        public ModificarServicio(IUnitOfWork unitOfWork, Repository<Domain.Entities.Cliente.Cliente> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public ServiceResponse Ejecutar(ServicesRequest request)
        {
            var cliente = _repository.FindBy(x => x.Id == request.Usuario_Id).FirstOrDefault();
            if (cliente != null)
            {
                var c = BuilderFactories.Cliente(request.Documento, request.Nombre, request.Email, request.Usuario_Id);

                cliente.Documento = c.Documento;
                cliente.Nombre = c.Nombre;
                cliente.Email = c.Email;
               
                _repository.Edit(cliente);
                _unitOfWork.Commit();
                return new ServiceResponse() { Mensaje = "Cliente Modificado con exito" };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "Cliente No existe" };
            }
        }
    }
}
