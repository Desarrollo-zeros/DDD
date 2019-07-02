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
        public Producto(string nombre, string descripción, string imagen, double precioCompra, double precioVenta, int cantidadProducto)
        {
            Nombre = nombre;
            Descripción = descripción;
            Imagen = imagen;
            if (!this.ComprobarPrecioCompra(precioCompra, precioVenta))
            {
                throw new Exception("El precio de compra no puede ser menor al precio de venta, debe ser mayor a 0");
            }
            if(!this.ComprobarPrecioVenta(precioCompra, precioVenta))
            {
                throw new Exception("El precio de Venta no puede ser menor al precio de Compra, debe ser mayor a 0");
            }
            this.PrecioCompra = precioCompra;
            this.PrecioVenta = precioVenta;
            CantidadProducto = cantidadProducto;
        }

        public string Nombre { set; get; }
        public string Descripción { set; get; }
        public  string Imagen { set; get; }
        public double PrecioCompra { set; get; }
        public double PrecioVenta { set; get; }
        public int CantidadProducto { set; get; }

        public virtual IEnumerable<ProductoDescuento> ProductoDescuentos { set; get; }

        public virtual IEnumerable<ProductoCliente> ProductoClientes { set; get; }

        public virtual IEnumerable<CompraEnvioProducto> CompraEnvioProductos { set; get; }



        public bool ComprobarPrecioCompra(double precioCompra,double precioVenta)
        {
            return (precioCompra > 0 && precioCompra < precioVenta);
        }

        public bool ComprobarPrecioVenta(double precioCompra, double precioVenta)
        {
            return (precioVenta > 0 && precioVenta > precioCompra);
        }


       
      
    }
}
