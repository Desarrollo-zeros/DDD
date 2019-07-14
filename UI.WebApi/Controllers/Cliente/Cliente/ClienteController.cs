using Application.Implements.Cliente.ServicioCliente;
using Domain.Factories;
using System;
using System.Linq;
using System.Web.Http;
using UI.WebApi.Generico;
using UI.WebApi.Models.ClienteModel.ClienteM;
using UI.WebApi.Models.ClienteModel.UsuarioM;

namespace UI.WebApi.Controllers.Cliente.Cliente
{
    [AllowAnonymous]
    [RoutePrefix("api/Cliente")]
    
    public class ClienteController : ApiController
    {
        private readonly UsuarioModel usuario;
        

        public ClienteController()
        {
            usuario = new UsuarioModel();
            if (usuario.id == 0)
            {
                throw new ArgumentException(Constants.NO_AUTH);
            }
        }


        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(ClienteModel cliente)
        {

            var FactoryCliente = BuilderFactories.Cliente(cliente.Cliente.Documento, cliente.Cliente.Nombre, cliente.Cliente.Email, usuario.id);
            var responseCliente = cliente.Create(new ServicioClienteRequest()
            {
                Documento = FactoryCliente.Documento,
                Email = FactoryCliente.Email,
                Nombre = FactoryCliente.Nombre,
                Usuario_Id = usuario.id
            });

            if (!responseCliente.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseCliente.Mensaje, Constants.CLIENT_FAIL));
            }

            cliente.Cliente.Telefónos.ToList().ForEach(x =>
            {
                cliente.telefónoModel.Create(new ServicioTelefónoRequest
                {
                    Cliente_Id = responseCliente.Id,
                    Número = x.Número,
                    TipoTelefono = x.TipoTelefono
                });
            });

            cliente.Cliente.Direcciónes.ToList().ForEach(x =>
            {
                cliente.direcciónModel.Create(new ServicioDireccíonRequest
                {
                    Cliente_Id = responseCliente.Id,
                    Barrio = x.Barrio,
                    Direccion = x.Direccion,
                    Municipio_Id = x.Municipio_Id,
                    CodigoPostal = x.CodigoPostal
                });
            });

            return Json(Mensaje.MensajeJson(Constants.NO_ERROR, responseCliente.Mensaje, Constants.CLIENT_SUCCESS));
        }


        [HttpPost]
        [Route("edit")]
        public IHttpActionResult Edit(ClienteModel cliente)
        {
            var FactoryCliente = BuilderFactories.Cliente(cliente.Cliente.Documento, cliente.Cliente.Nombre, cliente.Cliente.Email, usuario.id);
            var responseCliente = cliente.Edit(new ServicioClienteRequest()
            {
                Documento = FactoryCliente.Documento,
                Email = FactoryCliente.Email,
                Nombre = FactoryCliente.Nombre,
                Usuario_Id = usuario.id,
            });


            cliente.Cliente.Telefónos.ToList().ForEach(x =>
            {
                cliente.telefónoModel.Edit(new ServicioTelefónoRequest
                {
                    Cliente_Id = responseCliente.Id,
                    Número = x.Número,
                    TipoTelefono = x.TipoTelefono,
                    Id = x.Id
                    
                });
            });

            cliente.Cliente.Direcciónes.ToList().ForEach(x =>
            {
                cliente.direcciónModel.Edit(new ServicioDireccíonRequest
                {
                    Cliente_Id = responseCliente.Id,
                    Barrio = x.Barrio,
                    Direccion = x.Direccion,
                    Municipio = x.Municipio,
                    CodigoPostal = x.CodigoPostal,
                    Id = x.Id
                });
            });

            return Json(Mensaje.MensajeJson(Constants.NO_ERROR, responseCliente.Mensaje, Constants.CLIENT_SUCCESS));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get()
        {
            return Json(ClienteModel.Instance.Get(new ServicioClienteRequest { Usuario_Id = usuario.id}));
        }

        [HttpGet]
      
        [Route("get_all")]
        public IHttpActionResult GetAll()
        {
            return Json(ClienteModel.GetAll(usuario.id));
        }
    }
}
