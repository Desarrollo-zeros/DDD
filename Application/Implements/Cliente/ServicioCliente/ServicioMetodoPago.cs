using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Domain.Factories;

namespace Application.Implements.Cliente.ServicioCliente
{
    public class ServicioMetodoPago
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IGenericRepository<ClienteMetodoDePago> _repository;

        public ServicioMetodoPago(IUnitOfWork unitOfWork, IGenericRepository<ClienteMetodoDePago> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ServiceResponse Create(ServicioMetodoPagoRequest request)
        {
            var metodoPago = Get(request);

            if(metodoPago != null)
            {
                return new ServiceResponse
                {
                    Mensaje = "Metodo de pago ya existe",
                    Status = false
                };
            }
            var buildMetodoPago = BuilderFactories.ClienteMetodoDePago(request.Cliente_Id, request.Saldo, request.CreditCard.Type, request.CreditCard.CardNumber, request.CreditCard.SecurityNumber, request.CreditCard.OwnerName, request.CreditCard.ExpirationDate);
            metodoPago = _repository.Add(buildMetodoPago);

            if (_unitOfWork.Commit() == 1)
            {
                return new ServiceResponse
                {
                    Id = metodoPago.Id,
                    Mensaje = "Metodo de pago creado con exito",
                    Status = true
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Mensaje = "Metodo no pudo crearse",
                    Status = false
                };
            }

        }

        public ServiceResponse Edit(ServicioMetodoPagoRequest request)
        {
            try
            {
                var metodoPago = Get(new ServicioMetodoPagoRequest { Id = request.Id });

                if (metodoPago == null)
                {
                    return new ServiceResponse
                    {
                        Mensaje = "Metodo de pago no existe",
                        Status = false
                    };
                }

                metodoPago.Cliente.Usuario = request.Cliente.Usuario;

                if (metodoPago.Saldo > request.Saldo)
                {

                    if (!metodoPago.DescontarSaldo(request.Saldo))
                    {
                        return new ServiceResponse
                        {
                            Mensaje = "No tiene permiso para modificar su saldo, los datos no fueron modificados",
                            Status = false
                        };
                    }
                }
                if (metodoPago.Saldo < request.Saldo)
                {
                    if (!metodoPago.AumentarSaldo(request.Saldo))
                    {
                        return new ServiceResponse
                        {
                            Mensaje = "No tiene permiso para modificar su saldo, los datos no fueron modificados",
                            Status = false
                        };
                    }
                }

                metodoPago.Activo = request.Activo;
                metodoPago.CreditCard.ExpirationDate = request.CreditCard.ExpirationDate;
                metodoPago.CreditCard.OwnerName = request.CreditCard.OwnerName;
                metodoPago.CreditCard.SecurityNumber = request.CreditCard.SecurityNumber;
                metodoPago.CreditCard.Type = request.CreditCard.Type;

                if (metodoPago.CreditCard.CardNumber != request.CreditCard.CardNumber)
                {
                    var m = Get(new ServicioMetodoPagoRequest { CreditCard = request.CreditCard });
                    if (m != null)
                    {
                        return new ServiceResponse
                        {
                            Mensaje = "Numero de tarjeta ya esta en uso",
                            Status = false
                        };
                    }
                    else
                    {
                        metodoPago.CreditCard.CardNumber = request.CreditCard.CardNumber;
                    }
                }

                _repository.Edit(metodoPago);
                if(_unitOfWork.Commit() == 1)
                {
                    return new ServiceResponse
                    {
                        Mensaje = "Metodo de pago actualizado",
                        Status = false
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Mensaje = "No se puedo modificar el metodo de pago",
                        Status = false
                    };
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse
                {
                    Mensaje = "Metodo De Pago, "+e.Message,
                    Status = false
                };
            }
        }


        public ClienteMetodoDePago Get(ServicioMetodoPagoRequest request)
        {
            if (request.Id != 0 && request.CreditCard.CardNumber != null)
            {
                return _repository.FindBy(x => x.Id == request.Id && x.CreditCard.CardNumber == request.CreditCard.CardNumber).FirstOrDefault();
            }
            if (request.Id != 0)
            {
                return _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
            }
            if (null != request.CreditCard.CardNumber)
            {
                return _repository.FindBy(x => x.CreditCard.CardNumber == request.CreditCard.CardNumber).FirstOrDefault();
            }
            return null;
        }


        public IEnumerable<ClienteMetodoDePago> GetAll(ServicioMetodoPagoRequest request)
        {
            if(request.Id != 0 && request.CreditCard.CardNumber != null)
            {
                return _repository.FindBy(x => x.Id == request.Id && x.CreditCard.CardNumber == request.CreditCard.CardNumber).ToList();
            }
            if(request.Id != 0)
            {
                return _repository.FindBy(x => x.Id == request.Id ).ToList();
            }
            if(null != request.CreditCard.CardNumber)
            {
                return _repository.FindBy(x => x.CreditCard.CardNumber == request.CreditCard.CardNumber).ToList();
            }
            return null;
        }


    }

    public class ServicioMetodoPagoRequest : ClienteMetodoDePago
    {

    }
}
