
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
    public class EmpleadoService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmpleadoRepository _empleadoRepository;


        public EmpleadoService(IUnitOfWork unitOfWork, IEmpleadoRepository empleadoRepository)
        {
            _unitOfWork = unitOfWork;
            _empleadoRepository = empleadoRepository;
        }

        public CrearEmpleadoResponse Ejecutar(CrearEmpleadoRequest request)
        {
            Empleado empleado = _empleadoRepository.FindBy(t => t.Id.Equals(request.empleado.Id)).FirstOrDefault();
            if(empleado == null)
            {
                _empleadoRepository.Add(empleado);
                _unitOfWork.Commit();
                return new CrearEmpleadoResponse() { Mensaje = $"Empleado creada con exitos" };
            }
            else
            {
                return new CrearEmpleadoResponse() { Mensaje = $"El Empleado Ya existe" };
            }

        }

    }

    public class CrearEmpleadoRequest
    {
        public Empleado empleado { get; set; }
    }

    public class CrearEmpleadoResponse
    {
        public string Mensaje { get; set; }
    }

}
