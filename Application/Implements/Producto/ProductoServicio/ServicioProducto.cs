using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using Domain.Factories;

using Domain.Abstracts;
using Application.Base;
using Domain.Entities.Producto;
using Domain.ValueObjects;
using Domain.Enum;



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
