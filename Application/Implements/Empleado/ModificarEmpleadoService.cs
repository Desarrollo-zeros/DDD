
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
    public class ModificarEmpleadoService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmpleadoRepository _empleadoRepository;


        public ModificarEmpleadoService(IUnitOfWork unitOfWork, IEmpleadoRepository empleadoRepository)
        {
            _unitOfWork = unitOfWork;
            _empleadoRepository = empleadoRepository;
        }

        public ModificarEmpleadoResponse Ejecutar(ModificarEmpleadoRequest request)
        {
            Empleado empleado = _empleadoRepository.FindBy(t => t.Id.Equals(request.empleado.Id)).FirstOrDefault();
            if(empleado != null)
            {
                _empleadoRepository.Edit(empleado);
                _unitOfWork.Commit();
                return new ModificarEmpleadoResponse() { Mensaje = $"Empleado Modificada con exitos" };
            }
            else
            {
                return new ModificarEmpleadoResponse() { Mensaje = $"El Empleado no existe" };
            }

        }

    }

    public class ModificarEmpleadoRequest
    {
        public Empleado empleado { get; set; }
    }

    public class ModificarEmpleadoResponse
    {
        public string Mensaje { get; set; }
    }

}
