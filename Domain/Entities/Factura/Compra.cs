using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cliente;

namespace Domain.Entities.Factura
{
    public class Compra : Entity<int>
    {
        public Compra(int cliente_Id, DateTime fechaCompra, int comprobante_De_Pago_Id)
        {
            Cliente_Id = cliente_Id;
            FechaCompra = fechaCompra;
            Comprobante_De_Pago_Id = comprobante_De_Pago_Id;
        }

        public Compra() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")]  public Cliente.Cliente Cliente { set; get; }
        public DateTime FechaCompra { set; get; }
        public virtual IEnumerable<ProductoCliente> Productos { set; get; }

        public int Comprobante_De_Pago_Id { set; get; }
        [ForeignKey("Comprobante_De_Pago_Id")]  public ComprobanteDePago ComprobanteDePago { set; get; }

        
        public void DescontarTotalProductoEnSaldo(double saldo)
        {
            foreach(Cliente.ClienteMetodoDePago pagos in Cliente.ClienteMetodoDePagos.ToList())
            {
                if (pagos.DescontarSaldo(saldo))
                {
                    break;
                }
            }
        }


        public double CompraProducto(double valorProducto, int cantidad, int producto_Id)
        {
            double val = 1;
          
            foreach(ProductoCliente producto in Productos.ToList())
            {
                if (producto.Producto_Id == producto_Id) {
                   var descuentoProducto = producto.Producto.ProductoDescuentos.ToList();
                   
                    foreach (Producto.ProductoDescuento descuento in descuentoProducto.ToList<Producto.ProductoDescuento>())
                    {
                        var Fecha = descuento.Descuento.FechaYHoraTerminación;
                        if(Fecha.Year == DateTime.Now.Year && Fecha.Month == DateTime.Now.Month && Fecha.Day == DateTime.Now.Day && Fecha.Hour >= 23)
                        {
                            val += descuento.Descuento.Descu; 
                        }
                      
                    }
                    break;
                }
            }
            valorProducto += (valorProducto * val);
            double precioProducto = valorProducto * cantidad;
            return precioProducto;
        }

    }
}
