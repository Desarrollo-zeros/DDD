using Domain.Entities.Producto;

namespace UI.WebApi.Models.ProductoModel
{
    public class ProductoDescuentoModel : Model<ProductoDescuento>
    {
        public ProductoDescuento ProductoDescuento { set; get; }


        public static ProductoDescuentoModel Instance
        {
            get
            {
                return new ProductoDescuentoModel();
            }
        }
    }

}