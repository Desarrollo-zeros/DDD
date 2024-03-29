﻿using Application.Base;
using Domain.Abstracts;
using Domain.Factories;
using System.Linq;

namespace Application.Implements.Cliente.ServicioCliente
{
    public class ServicioCliente
    {
        readonly IUnitOfWork _unitOfWork;
        public readonly IGenericRepository<Domain.Entities.Cliente.Cliente> _repository;

        public ServicioCliente(IUnitOfWork unitOfWork, IGenericRepository<Domain.Entities.Cliente.Cliente> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ServiceResponse Create(ServicioClienteRequest request)
        {
            var cliente = Get(request);

            if (cliente == null)
            {
                if (_repository.FindBy(x => x.Usuario_Id == request.Usuario_Id).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Cliente ya existe", Status = false };
                }

                if (_repository.FindBy(x => x.Documento == request.Documento).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Documento ya existe", Status = false };
                }
                if (_repository.FindBy(x => x.Email == request.Email).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Email ya existe", Status = false };
                }

                var builderCLient = BuilderFactories.Cliente(request.Documento, request.Nombre, request.Email, request.Usuario_Id);

                cliente = _repository.Add(builderCLient);

                if (_unitOfWork.Commit() == 1)
                {

                    return new ServiceResponse() { Mensaje = "Cliente creado exito", Status = true, Id = cliente.Id };
                }
                else
                {
                    return new ServiceResponse() { Mensaje = "No se pudo crear cliente", Status = false };
                }
            }
            return new ServiceResponse() { Mensaje = "Cliente ya existe", Status = false };
        }


        public Domain.Entities.Cliente.Cliente Get(ServicioClienteRequest request)
        {
            if (request.Id != 0 && request.Documento != null && request.Nombre != null && request.Email != null && request.Usuario_Id != 0)
            {
                return _repository.FindBy(x =>
                        x.Id == request.Id &&
                        x.Usuario_Id == request.Usuario_Id &&
                        x.Documento == request.Documento &&
                        x.Nombre.PrimerNombre == request.Nombre.PrimerNombre &&
                        x.Nombre.PrimerApellido == request.Nombre.PrimerApellido &&
                        x.Nombre.SegundoApellido == request.Nombre.SegundoApellido &&
                        x.Email == request.Email
                    )
                    .FirstOrDefault();
            }
            else
            {
                if (request.Id != 0)
                {
                    return _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
                }
                else if (request.Documento != null)
                {
                    return _repository.FindBy(x => x.Documento == request.Documento).FirstOrDefault();
                }
                else if (request.Email != null)
                {
                    return _repository.FindBy(x => x.Email == request.Email).FirstOrDefault();
                }
                else if (request.Usuario_Id != 0)
                {
                    return _repository.FindBy(x => x.Usuario_Id == request.Usuario_Id).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }

        }

        public ServiceResponse Edit(ServicioClienteRequest request)
        {

            var cliente = Get(new ServicioClienteRequest { Usuario_Id = request.Usuario_Id });

            if (cliente == null)
            {
                return new ServiceResponse() { Mensaje = "Cliente no existe", Status = false };
            }

            if (cliente.Documento != request.Documento)
            {
                if (_repository.FindBy(x => x.Documento == request.Documento && x.Usuario_Id != cliente.Usuario_Id).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Documento ya existe", Status = false };
                }
                else
                {
                    cliente.Documento = request.Documento;
                }
            }

            if (cliente.Email != request.Email)
            {

                if (_repository.FindBy(x => x.Email == request.Email && x.Usuario_Id != cliente.Usuario_Id).FirstOrDefault() != null)
                {
                    return new ServiceResponse() { Mensaje = "Email ya existe", Status = false };
                }
                else
                {
                    cliente.Email = request.Email;
                }
            }

            cliente.Nombre = request.Nombre;
            _repository.Edit(cliente);

            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse() { Mensaje = "Cliente Modificado con exito", Status = true, Id = cliente.Id };
            }
            else
            {
                return new ServiceResponse() { Mensaje = "No se pudo Modificar cliente", Status = false };
            }
        }


    }

    public class ServicioClienteRequest : Domain.Entities.Cliente.Cliente
    {

    }

}
