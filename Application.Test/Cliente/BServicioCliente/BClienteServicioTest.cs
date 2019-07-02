using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implements.Cliente.ServicioCliente;
using Domain.Abstracts;
using Infraestructure.Data.Base;
using Infraestructure.Data;
using NUnit.Framework;
using Application.Base;
using Application.Implements;
using Domain.Factories;
using Domain.Entities.Cliente;
using Infraestructure.Data.Repositories;

namespace Application.Test.Cliente.BServicioCliente
{
    [TestFixture]
    class BClienteServicioTest
    {
        IUnitOfWork _unitOfWork;
        IDbContext _db;
        Repository<Domain.Entities.Cliente.Cliente> repository;
        CrearServicio crearServicio;
        ModificarServicio modificarServicio;
        EliminarServicio eliminarServicio;

      [SetUp()]

        public void Initializar()
        {
            _db = new DBContext();
            _unitOfWork = new UnitOfWork(_db);
            repository = new Repository<Domain.Entities.Cliente.Cliente>(_db);
            crearServicio = new CrearServicio(_unitOfWork, repository);
            modificarServicio = new ModificarServicio(_unitOfWork, repository);
            eliminarServicio = new EliminarServicio(_unitOfWork, repository);
        }


     
        [Test]
        public void CrearUsuarioNoExisteTest()
        {
            var response = crearServicio.Ejecutar(new ServicesRequest() { Documento = "1063969856", Nombre = "Carlos Adrés Castilla García", Email = "carloscastilla31@gmail.com", Usuario_Id = 2 });
            Assert.AreEqual(response.Mensaje, "Cliente Creado Con exito");

        }


        [Test]
        public void CrearUsuarioSiExisteTest()
        {
            
            var response = crearServicio.Ejecutar(new ServicesRequest() { Documento = "1063969856", Nombre = "Carlos Adrés Castilla García", Email = "carloscastilla31@gmail.com", Usuario_Id = 2 });
            Assert.AreEqual(response.Mensaje, "Cliente Ya existe");
        }

        [Test]
        public void DModificarUsuarioExisteTest()
        {
            var response = modificarServicio.Ejecutar(new ServicesRequest() { Documento = "1063969856", Nombre = "Carlos Adrés Castilla García", Email = "carloscastilla31@gmail.com", Usuario_Id = 2 });
            Assert.AreEqual(response.Mensaje, "Cliente Modificado con exito");
        }

        [Test]
        public void DModificarUsuarioNoExisteTest()
        {
            var response = modificarServicio.Ejecutar(new ServicesRequest() { Documento = "1063969856", Nombre = "Carlos Adrés Castilla García", Email = "carloscastilla31@gmail.com", Usuario_Id = 100 });
            Assert.AreEqual(response.Mensaje, "Cliente No existe");
        }


        //[Test]
        public void EliminarUsuarioExisteTest()
        {

            var response = eliminarServicio.Ejecutar(new ServicesRequest() { Usuario_Id = repository.GetAll().ToList().FirstOrDefault().Id});
            Assert.AreEqual(response.Mensaje, "Cliente Eliminado con existos");
        }

        //[Test]
        public void EliminarUsuarioNoExisteTest()
        {
            var response = eliminarServicio.Ejecutar(new ServicesRequest() { Usuario_Id = 100 });
            Assert.AreEqual(response.Mensaje, "Cliente No existe");
        }

    }
}
