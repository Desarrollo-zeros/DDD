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
    
    public class CalcularService : IOperacionPrima
    {
         readonly IUnitOfWork _unitOfWork;
         readonly IPrimaRepository _primaRepository;
       
        public CalcularService(IUnitOfWork unitOfWork, IPrimaRepository primaRepository)
        {
            _unitOfWork = unitOfWork;
            _primaRepository = primaRepository;
          
        }

        public CalcularResponse Ejecutar(CalcularRequest request)
        {
            Prima prima = _primaRepository.FindBy(t => t.empleadoId.Equals(request.prima.empleadoId)).FirstOrDefault();

            if(prima != null)
            {
                prima.calcular(request.empleado.salario, request.empleado.dias, request.empleado.año);
                _primaRepository.Edit(prima);
                _unitOfWork.Commit();
                return new CalcularResponse() { Mensaje = $"" };
            }else{
                return new CalcularResponse() { Mensaje = $"" };
            }
        }
    }

    public class CalcularRequest
    {
        public Prima prima;
        public Empleado empleado;

    }
    public class CalcularResponse
    {
        public string Mensaje { get; set; }
    }

}
