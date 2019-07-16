﻿using Application.Implements.Cliente.ServicioUsuario;
using Domain.Enum;
using Domain.Factories;
using System;
using System.Threading;
using System.Web.Http;
using UI.WebApi.Generico;
using UI.WebApi.Models.ClienteModel.UsuarioM;


namespace UI.WebApi.Controllers.Cliente.Usuario
{
    [AllowAnonymous]
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
       
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }


        [HttpPost]
        [Route("Autenticate")]
        public IHttpActionResult Autenticar(UsuarioModel usuario)
        {
            if (usuario.Usuario == null)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Objecto no puede estar vacio", Constants.USER_FAIL));
            }


            if (usuario == null || usuario.Usuario.Username == null || usuario.Usuario.Password == null)
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, Constants.USER_INVALID, Constants.USER_FAIL));

            try
            {
                var factoryUser = BuilderFactories.Usuario(usuario.Usuario.Username, usuario.Usuario.Password, true, Domain.Enum.Rol.INVITADO);
                var user = usuario.ServicioUsuario.Autenticar(new ServicioUsuarioRequest() { Username = factoryUser.Username, Password = factoryUser.Password });


                if (user == null)
                    return Unauthorized();

                if (user.Activo == false)
                {
                    return Json(Mensaje.MensajeJson(Constants.IS_ERROR, Constants.USER_INACTIVE, Constants.USER_FAIL));
                }

                var token = TokenGenerator.GenerateTokenJwt(usuario.Usuario.Username);
                return Ok((Mensaje.MensajeJson(Constants.NO_ERROR, "Token => "+token, Constants.USER_SUCCESS)));
            }
            catch (Exception e)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, e.Message, Constants.USER_INVALID));
            }
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Crear(UsuarioModel usuario)
        {
            if (usuario.Usuario == null)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Objecto no puede estar vacio", Constants.USER_FAIL));
            }

            try
            {
                var factoryUser = BuilderFactories.Usuario(usuario.Usuario.Username, usuario.Usuario.Password, true, Rol.INVITADO);
                var response = usuario.ServicioUsuario.Create(new ServicioUsuarioRequest { Username = factoryUser.Username, Password = factoryUser.Password, Rol = Rol.CLIENTE, Activo = true });

                if (!response.Status)
                {
                    return Json(Mensaje.MensajeJson(Constants.IS_ERROR, response.Mensaje, Constants.USER_FAIL));

                }
                return Json(Mensaje.MensajeJson(Constants.NO_ERROR, response.Mensaje, Constants.USER_SUCCESS));

            }
            catch (Exception e)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, e.Message, Constants.USER_INVALID));
            }
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get()
        {
            if (UsuarioModel.Instance.Auth().IsAuthenticated)
            {
                return Json(UsuarioModel.Get(UsuarioModel.Instance.id));
            }
            else
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, Constants.NO_AUTH, Constants.USER_FAIL));
            }
        }

    }
}
