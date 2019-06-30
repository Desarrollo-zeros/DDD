using Domain.Entities.Factura;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Producto
{
    public class Producto : Entity<int>
    {
        public Producto() { }
        public Producto(string nombre, string descripción, Imagen imagen, double precioCompra, double precioVenta)
        {
            Nombre = nombre;
            Descripción = descripción;
            Imagen = imagen;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
        }

        public string Nombre { set; get; }
        public string Descripción { set; get; }
        public  Imagen Imagen { set; get; }
        public double precioCompra { set; get; }
        public double precioVenta { set; get; }

        public virtual IEnumerable<ProductoDescuento> ProductoDescuentos { set; get; }

        public IEnumerable<ProductoCliente> productos { set; get; }
    }
}
