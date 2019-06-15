
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
    public class ModificarPersonaService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IPersonaRepository _personaRepository;


        public ModificarPersonaService(IUnitOfWork unitOfWork, IPersonaRepository personaRepository)
        {
            _unitOfWork = unitOfWork;
            _personaRepository = personaRepository;
        }

        public ModificarPersonaResponse Ejecutar(ModificarPersonaRequest request)
        {
            Persona persona = _personaRepository.FindBy(t => t.documento.Equals(request.persona.documento)).FirstOrDefault();
            if(persona != null)
            {
                _personaRepository.Edit(persona);
                _unitOfWork.Commit();
                return new ModificarPersonaResponse() { Mensaje = $"Persona Modificada con exitos" };
            }
            else
            {
                return new ModificarPersonaResponse() { Mensaje = $"La persona no existe" };
            }

        }

    }

    public class ModificarPersonaRequest
    {
        public Persona persona { get; set; }
    }

    public class ModificarPersonaResponse
    {
        public string Mensaje { get; set; }
    }

}
