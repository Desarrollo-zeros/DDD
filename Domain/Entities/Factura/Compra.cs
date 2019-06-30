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
        public virtual IEnumerable<ProductoCliente> ProductoCliente { set; get; }

        public int Comprobante_De_Pago_Id { set; get; }
        [ForeignKey("Comprobante_De_Pago_Id")]  public ComprobanteDePago ComprobanteDePago { set; get; }

        public bool ComprarArticulos(List<ProductoCliente> productos, int cantidad)
        {
            double descuentoTotal = 0;
            double precioVenta = 0;

            int cantidadProductoNoExistentes = 0;

            if(productos.Count() == 0)
            {
                throw new Exception("No hay productos para realizar la compra");
            }
            foreach (var p in productos)
            {
                if(!SePuedeComprarProducto(p.Producto.CantidadProducto,cantidad))
                {
                    cantidadProductoNoExistentes += 1;
                    
                }
                else
                {
                    descuentoTotal += ObtenerDescuentoPorProductoCompra(p.Cliente_Id, p.Producto_Id, cantidad);
                    precioVenta += p.Producto.PrecioVenta;
                    p.EstadoProductoCliente = true;
                }
            }

            if (!this.DescontarTotalProductoEnSaldo(precioVenta - descuentoTotal))
            {
                throw new Exception("No se puede completar la compra, no tiene sufienciente saldo");
            }

            if(cantidadProductoNoExistentes > 0)
            {
                throw new Exception("Algunos articulos no puedieron ser comprados");
            }
            return true;
        }

        public bool DescontarTotalProductoEnSaldo(double saldo)
        {
            foreach(Cliente.ClienteMetodoDePago pagos in Cliente.ClienteMetodoDePagos.ToList())
            {
                if (pagos.Activo && pagos.Saldo > saldo)
                {
                    if (pagos.DescontarSaldo(saldo))
                    {
                        ComprobanteDePago.EstadoDePago = Enum.EstadoDePago.PAGADO;
                        return true;
                    }
                }
            }
            return false;
        }


        public double ObtenerDescuentoPorProductoCompra(int Cliente_Id, int producto_Id, int cantidad)
        {
            var sumaDescuento = 0.0;
            var valorProducto = 0.0;
            bool r = false;

            foreach(ProductoCliente productoCliente in ProductoCliente)
            {
                if(productoCliente.Producto_Id == producto_Id && productoCliente.Cliente_Id == Cliente_Id)
                {
                    foreach (Producto.ProductoDescuento productoDescuento in productoCliente.Producto.ProductoDescuentos)
                    {
                        if(productoDescuento.Descuento.DescuentoEsAplicable(productoDescuento.Descuento.FechaYHoraInicio, productoDescuento.Descuento.FechaYHoraTerminación))
                        {
                            sumaDescuento += productoDescuento.Descuento.Descu;
                            
                        }
                    }
                    valorProducto = productoCliente.Producto.PrecioVenta;
                    r = true;
                    break;
                }
            }
            if(!r) throw new Exception("El producto Y/o el cliente no existen");
            return valorProducto * cantidad * sumaDescuento;
        }

        public bool SePuedeComprarProducto(int CantidadProducto, int cantidad)
        {
            return CantidadProducto > cantidad && CantidadProducto > 0 ;
        }
        

    }
}
