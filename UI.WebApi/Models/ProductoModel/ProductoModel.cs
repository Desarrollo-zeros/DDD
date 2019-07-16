using Domain.Entities.Producto;
using Newtonsoft.Json;

namespace UI.WebApi.Models.ProductoModel
{
    public class ProductoModel : Model<Producto>
    {
        public Producto Producto { set; get; }

        [JsonIgnore]
        private static ProductoModel productoModel;
        public static ProductoModel Instance
        {
            get
            {
                if (productoModel == null)
                {
                    productoModel = new ProductoModel();
                }
                return productoModel;
            }
        }
    }

}