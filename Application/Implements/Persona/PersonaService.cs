
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
    public class PersonaService : IOperacionPrima
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IPersonaRepository _personaRepository;


        public PersonaService(IUnitOfWork unitOfWork, IPersonaRepository personaRepository)
        {
            _unitOfWork = unitOfWork;
            _personaRepository = personaRepository;
        }

        public CrearPersonaResponse Ejecutar(CrearPersonaRequest request)
        {
            Persona persona = _personaRepository.FindBy(t => t.documento.Equals(request.persona.documento)).FirstOrDefault();
            if(persona == null)
            {
                _personaRepository.Add(persona);
                _unitOfWork.Commit();
                return new CrearPersonaResponse() { Mensaje = $"Persona creada con exitos" };
            }
            else
            {
                return new CrearPersonaResponse() { Mensaje = $"La persona Ya existe" };
            }

        }

    }

    public class CrearPersonaRequest
    {
        public Persona persona { get; set; }
    }

    public class CrearPersonaResponse
    {
        public string Mensaje { get; set; }
    }

}
