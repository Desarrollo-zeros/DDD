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
        public IHttpActionResult Crear(ClienteModel cliente)
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
           var responseTelefóno = cliente.telefónoModel.Create(new ServicioTelefónoRequest() { Cliente_Id = responseCliente.Id, Telefónos = BuilderFactories.Telefónos(cliente.Cliente.Telefónos, responseCliente.Id) });
           var responseDirecciónes = cliente.direcciónModel.Create(new ServicioDireccíonRequest() { Cliente_Id = responseCliente.Id, Direcciónes = BuilderFactories.Direcciónes(cliente.Cliente.Direcciónes, responseCliente.Id) });

            if (!responseTelefóno.Status && !responseDirecciónes.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, Constants.CLIENT_DIRECCIÓN_TELEFÓNO_FAIL, Constants.CLIENT_SUCCESS));
            }

            if (!responseTelefóno.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseTelefóno.Mensaje, Constants.CLIENT_SUCCESS));
            }
            if (!responseDirecciónes.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseDirecciónes.Mensaje, Constants.CLIENT_SUCCESS));
            }

            return Json(Mensaje.MensajeJson(Constants.NO_ERROR, responseCliente.Mensaje, Constants.CLIENT_SUCCESS));
        }


        [HttpPost]
        [Route("edit")]
        public IHttpActionResult Modificar(ClienteModel cliente)
        {
            var FactoryCliente = BuilderFactories.Cliente(cliente.Cliente.Documento, cliente.Cliente.Nombre, cliente.Cliente.Email, usuario.id);
            var responseCliente = cliente.Edit(new ServicioClienteRequest()
            {
                Documento = FactoryCliente.Documento,
                Email = FactoryCliente.Email,
                Nombre = FactoryCliente.Nombre,
                Usuario_Id = usuario.id,
            });

            var responseTelefóno = cliente.telefónoModel.Edit(new ServicioTelefónoRequest() {Telefónos = BuilderFactories.Telefónos(cliente.Cliente.Telefónos, responseCliente.Id) });
            var responseDirecciónes = cliente.direcciónModel.Edit(new ServicioDireccíonRequest() { Direcciónes = BuilderFactories.Direcciónes(cliente.Cliente.Direcciónes, responseCliente.Id) });

            if (!responseTelefóno.Status && !responseDirecciónes.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, Constants.CLIENT_DIRECCIÓN_TELEFÓNO_FAIL, Constants.CLIENT_SUCCESS));
            }

            if (!responseTelefóno.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseTelefóno.Mensaje, Constants.CLIENT_SUCCESS));
            }
            if (!responseDirecciónes.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseDirecciónes.Mensaje, Constants.CLIENT_SUCCESS));
            }


            if (!responseCliente.Status)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, responseCliente.Mensaje, Constants.CLIENT_FAIL));

            }
            return Json(Mensaje.MensajeJson(Constants.NO_ERROR, responseCliente.Mensaje, Constants.CLIENT_SUCCESS));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Obtener()
        {
            return Json(ClienteModel.Instance.repository.FindBy(x=>x.Usuario_Id == usuario.id).ToList());
        }

        [HttpGet]
      
        [Route("get_all")]
        public IHttpActionResult ObtenerTodo()
        {
            return Json(ClienteModel.GetAll(usuario.id));
        }
    }
}
