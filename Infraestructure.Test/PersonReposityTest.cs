using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Data.Repositories;
using NUnit.Framework;


namespace Infraestructure.Test
{
    [TestFixture]
    public class PersonReposityTest
    {
        PrimaContextTest databaseContext;
        PersonaRepository objRepo;
        public Persona persona;

        [SetUp]
        public void Initialize()
        {
            Console.WriteLine("Inicalizando");
            databaseContext = new PrimaContextTest();
            objRepo = new PersonaRepository(databaseContext);
            //persona = new Persona("1063969856", "carlos", "andres", "castilla", "garcia", 21);

        }

        [Test]
        public void Persona_add()
        {
            Console.WriteLine("Ejecutando Persona add");
            var result = objRepo.Add(persona);
            databaseContext.SaveChanges();
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Persona_del()
        {
            Console.WriteLine("Ejecutando Persona add");
           
            Persona personas = objRepo.FindBy(t => t.documento.Equals("1063969856")).FirstOrDefault();
            if(personas != null)
            {
                objRepo.Delete(personas);
                databaseContext.SaveChanges();  
            }
            Persona persona = objRepo.FindBy(t => t.documento.Equals("1063969856")).FirstOrDefault();
            Assert.AreEqual(persona, null);
        }


    }
}
