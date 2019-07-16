using Domain.Enum;
using Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.WebApi.Generico;
using UI.WebApi.Models.ClienteModel.UsuarioM;
using UI.WebApi.Models.ProductoModel;

namespace UI.WebApi.Controllers.Producto
{
    [AllowAnonymous]
    [RoutePrefix("api/Producto")]
    public class ProductoController : ApiController
    {

        [HttpPost]
        [Route("categoria_create")]
        public IHttpActionResult Create(CategoriaModel categoriaModel)
        {
        
            if (UsuarioModel.Instance.rol  != Rol.ADMINISTRADOR && UsuarioModel.Instance.rol != Rol.DEV)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "No esta autorizado para realizar esta operacion", Constants.NO_AUTH));
            }

            if (categoriaModel.Categoria == null)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Objecto no puede estar vacio", Constants.CATEGORIA_FAIL));
            }

            var categoria = categoriaModel._repository.FindBy(x => x.Nombre == categoriaModel.Categoria.Nombre).FirstOrDefault();
            if(categoria == null)
            {
                var c = categoriaModel.Create(BuilderFactories.Categoria(categoriaModel.Categoria.Nombre, categoriaModel.Categoria.Descripción, (categoriaModel.Categoria.FechaCreacion.Year < DateTime.Now.Year) ? DateTime.Now : categoriaModel.Categoria.FechaCreacion));
                if (c != null)
                {
                    return Json(Mensaje.MensajeJson(Constants.NO_ERROR, "Categoria creada con exito", Constants.CATEGORIA_SUCCES));
                }
                else
                {
                    return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Error al crear la categoria", Constants.CATEGORIA_FAIL));
                }
            }
            else
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Categoria ya existe", Constants.CATEGORIA_FAIL));
            }
        }


        [HttpPost]
        [Route("categoria_Edit")]
        public IHttpActionResult Edit(CategoriaModel categoriaModel)
        {
           
            if (categoriaModel.Categoria == null)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Objecto no puede estar vacio", Constants.CATEGORIA_FAIL));
            }

            var categoria = categoriaModel._repository.FindBy(x => x.Nombre == categoriaModel.Categoria.Nombre && x.Id != categoriaModel.Categoria.Id).FirstOrDefault();

            if(categoria != null)
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Nombre Categoria ya existe", Constants.CATEGORIA_FAIL));
            }

            categoria = categoriaModel._repository.FindBy(x => x.Id == categoriaModel.Categoria.Id).FirstOrDefault();
            if (categoria != null)
            {
                categoria.Nombre = categoriaModel.Categoria.Nombre;
                categoria.Descripción = categoria.Descripción;
                if (categoriaModel.Update(categoria))
                {
                    return Json(Mensaje.MensajeJson(Constants.NO_ERROR, "Categoria Modificada con exito", Constants.CATEGORIA_SUCCES));
                }
                else
                {
                    return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Error al modificar la categoria", Constants.CATEGORIA_FAIL));
                }
            }
            else
            {
                return Json(Mensaje.MensajeJson(Constants.IS_ERROR, "Categoria no existe", Constants.CATEGORIA_FAIL));
            }
        }


        [HttpGet]
        [Route("categoria_get/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Json(CategoriaModel.Instance.Find(id));
        }

        [HttpGet]
        [Route("categoria_get_all")]
        public IHttpActionResult GetAll()
        {
            return Json(CategoriaModel.Instance.GetAll());
        }



        /*******************************/


    }
}
