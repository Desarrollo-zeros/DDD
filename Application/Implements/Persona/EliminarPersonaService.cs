
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
    public class EliminarPersonaService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IPersonaRepository _personaRepository;


        public EliminarPersonaService(IUnitOfWork unitOfWork, IPersonaRepository personaRepository)
        {
            _unitOfWork = unitOfWork;
            _personaRepository = personaRepository;
        }

        public EliminarPersonaResponse Ejecutar(EliminarPersonaRequest request)
        {
            Persona persona = _personaRepository.FindBy(t => t.documento.Equals(request.persona.documento)).FirstOrDefault();
            if(persona != null)
            {
                _personaRepository.Delete(persona);
                _unitOfWork.Commit();
                return new EliminarPersonaResponse() { Mensaje = $"Persona Eliminada con exitos" };
            }
            else
            {
                return new EliminarPersonaResponse() { Mensaje = $"La persona No existe" };
            }

        }

    }

    public class EliminarPersonaRequest
    {
        public Persona persona { get; set; }
    }

    public class EliminarPersonaResponse
    {
        public string Mensaje { get; set; }
    }

}
