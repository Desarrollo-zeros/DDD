using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implements.Cliente.ServicioUsuario;
using Domain.Abstracts;
using Infraestructure.Data.Base;
using Infraestructure.Data;
using NUnit.Framework;
using Application.Base;
using Application.Implements;
using Domain.Factories;
using Domain.Entities.Cliente;
using Infraestructure.Data.Repositories;

namespace Application.Test.Cliente.AServicioUsuario
{
    [TestFixture]
    class AUsuarioServicioTest
    {
        IUnitOfWork _unitOfWork;
        IDbContext _db;
        Repository<Usuario> repository;
        CrearServicio crearServicio;
        ModificarServicio modificarServicio;
        EliminarServicio eliminarServicio;

       [SetUp()]

        public void Initializar()
        {
            _db = new DBContext();
            _unitOfWork = new UnitOfWork(_db);
            repository = new Repository<Usuario>(_db);
            crearServicio = new CrearServicio(_unitOfWork, repository);
            modificarServicio = new ModificarServicio(_unitOfWork, repository);
            eliminarServicio = new EliminarServicio(_unitOfWork, repository);
          
        }

     
        [Test]
        public void CrearUsuarioNoExisteTest()
        {
            var response = crearServicio.Ejecutar(new ServicesRequest() { Username = "test", Password = "test", Activo = true });
            Assert.AreEqual(response.Mensaje, "Usuario Creado Con exito");

        }


        [Test]
        public void CrearUsuarioSiExisteTest()
        {
            var response = crearServicio.Ejecutar(new ServicesRequest() { Username = "test", Password = "test", Activo = true });
            Assert.AreEqual(response.Mensaje, "Usuario Ya existe");
        }

        [Test]
        public void DModificarUsuarioExisteTest()
        {
            var response = modificarServicio.Ejecutar(new ServicesRequest() { Id = repository.GetAll().ToList().FirstOrDefault().Id, Username = "test",  Password = "toor", Activo = true });
            Assert.AreEqual(response.Mensaje, "Usuario Modificado con exito");
        }

        [Test]
        public void DModificarUsuarioNoExisteTest()
        {
            var response = modificarServicio.Ejecutar(new ServicesRequest() { Id = 100, Username = "test", Password = "toor", Activo = true });
            Assert.AreEqual(response.Mensaje, "Usuario No existe");
        }


        //[Test]
        public void EliminarUsuarioExisteTest()
        {

            var response = eliminarServicio.Ejecutar(new ServicesRequest() { Id = repository.GetAll().ToList().FirstOrDefault().Id});
            Assert.AreEqual(response.Mensaje, "Usuario Eliminado con existos");
        }

        //[Test]
        public void EliminarUsuarioNoExisteTest()
        {
            var response = eliminarServicio.Ejecutar(new ServicesRequest() { Id = 100 });
            Assert.AreEqual(response.Mensaje, "Usuario No existe");
        }

    }
}
