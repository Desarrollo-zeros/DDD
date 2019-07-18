
using Application.Base;
using Domain.Abstracts;
using Domain.Entities.Producto;



namespace Application.Implements.Producto.ProductoServicio
{
    public class ServicioProducto : EntityService<Domain.Entities.Producto.Producto>
    {

        public ServicioProducto(IUnitOfWork unitOfWork, IGenericRepository<Domain.Entities.Producto.Producto> repository) : base(unitOfWork, repository)
        {
        }


    }


    public class ProductoRequest : Domain.Entities.Producto.Producto
    {

    }



    public class DescuentoServicio
    {

    }

    public class DescuentoRequest : Descuento
    {


    }


    public class ProductoDescuentoServicio
    {


    }

    public class ProductoDescuentoRequest : ProductoDescuento
    {

    }

}
