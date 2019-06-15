using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implements;
using Domain.Abstracts;
using Domain.Entities;
using Infraestructure.Data.Base;
using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Moq;
using NUnit.Framework;


namespace Application.Test
{
    [TestFixture]
    public class PersonaServiceTest
    {
        IUnitOfWork _unitOfWork;
        IDbContext _db;
        Persona persona;
        IPersonaRepository _personaRepository;

        [SetUp]
        public void Initializar()
        {
            _db = new PrimaContext();
            persona = new Persona("1234567890","carlos","andres","castilla","garcia",21);
            _db.Set<Persona>().Add(persona);
            _db.SaveChanges();
            _personaRepository = new PersonaRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }


        [Test]
        public void newPerson()
        {
            PersonaService personaService = new PersonaService(_unitOfWork, _personaRepository);
            CrearPersonaResponse resp = personaService.Ejecutar(new CrearPersonaRequest() { persona = this.persona });
            Console.WriteLine(resp);
            Assert.AreEqual($"Persona creada con exitos", resp.Mensaje);
        }
    }
}
