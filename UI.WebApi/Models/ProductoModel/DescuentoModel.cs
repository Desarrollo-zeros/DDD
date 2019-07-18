using Domain.Entities.Producto;

namespace UI.WebApi.Models.ProductoModel
{
    public class DescuentoModel : Model<Descuento>
    {
        public Descuento Descuento { set; get; }


        public static DescuentoModel Instance
        {
            get
            {
                return new DescuentoModel();
            }
        }
    }

}