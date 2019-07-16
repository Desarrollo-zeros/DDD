using Domain.Entities.Producto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.WebApi.Models.ProductoModel
{
    public class CategoriaModel : Model<Categoria>
    {
        public Categoria Categoria { set; get; }

        [JsonIgnore]
        private static CategoriaModel categoriaModel;
        public static CategoriaModel Instance
        {
            get
            {
                if (categoriaModel == null)
                {
                    categoriaModel = new CategoriaModel();
                }
                return categoriaModel;
            }
        }
    }

}