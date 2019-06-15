
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base;
using Domain.Abstracts;
using Domain.Entities;

namespace Application.Implements
{
    public class EliminarEmpleadoService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmpleadoRepository _empleadoRepository;


        public EliminarEmpleadoService(IUnitOfWork unitOfWork, IEmpleadoRepository empleadoRepository)
        {
            _unitOfWork = unitOfWork;
            _empleadoRepository = empleadoRepository;
        }

        public EliminarEmpleadoResponse Ejecutar(EliminarEmpleadoRequest request)
        {
            Empleado empleado = _empleadoRepository.FindBy(t => t.Id.Equals(request.empleado.Id)).FirstOrDefault();
            if(empleado != null)
            {
                _empleadoRepository.Add(empleado);
                _unitOfWork.Commit();
                return new EliminarEmpleadoResponse() { Mensaje = $"Empleado Eliminado con exitos" };
            }
            else
            {
                return new EliminarEmpleadoResponse() { Mensaje = $"El empleado no existe" };
            }

        }

    }

    public class EliminarEmpleadoRequest
    {
        public Empleado empleado { get; set; }
    }

    public class EliminarEmpleadoResponse
    {
        public string Mensaje { get; set; }
    }

}
