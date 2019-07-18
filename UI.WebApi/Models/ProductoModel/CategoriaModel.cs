using Domain.Entities.Producto;

namespace UI.WebApi.Models.ProductoModel
{
    public class CategoriaModel : Model<Categoria>
    {
        public Categoria Categoria { set; get; }


        public static CategoriaModel Instance
        {
            get
            {
                return new CategoriaModel();
            }
        }
    }

}