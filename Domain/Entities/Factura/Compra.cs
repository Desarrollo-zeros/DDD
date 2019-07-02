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
            CantidadProductoNoExistentes = 0;
        }

        public Compra() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")]  public Cliente.Cliente Cliente { set; get; }
        public DateTime FechaCompra { set; get; }
        public virtual IEnumerable<ProductoCliente> ProductoCliente { set; get; }

        public virtual IEnumerable<CompraEnvio> CompraEnvios { set; get; }

        public int Comprobante_De_Pago_Id { set; get; }
        [ForeignKey("Comprobante_De_Pago_Id")]  public ComprobanteDePago ComprobanteDePago { set; get; }



        [NotMapped]
        public int CantidadProductoNoExistentes { private set; get; }

        public bool ComprarArticulos(List<ProductoCliente> productos, int cantidad)
        {
            double descuentoTotal = 0;
            double precioVenta = 0;

            

            if(productos.Count() == 0)
            {
                throw new Exception("No hay productos para realizar la compra");
            }
            foreach (var p in productos)
            {
                if(!SePuedeComprarProducto(p.Producto.CantidadProducto,cantidad))
                {
                    this.CantidadProductoNoExistentes = cantidad-p.Producto.CantidadProducto;
                    
                }
                else
                {
                    descuentoTotal += ObtenerDescuentoPorProductoCompra(p.Cliente_Id, p.Producto_Id, cantidad);
                    precioVenta += p.Producto.PrecioVenta;
                    p.EstadoProductoCliente = Enum.EstadoClienteArticulo.PAGADO;
                }
            }

            if (!this.DescontarTotalProductoEnSaldo(precioVenta - descuentoTotal) && this.CantidadProductoNoExistentes == 0)
            {
                throw new Exception("No se puede completar la compra, no tiene sufienciente saldo");
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
            return CantidadProducto > cantidad;
        }
       
        public bool EnviarCompra(int compra_id, int producto_id)
        {
            if(ComprobanteDePago.EstadoDePago == Enum.EstadoDePago.PAGADO)
            {
                CompraEnvio compraEnvio = CompraEnvios.ToList().Find(x => x.Compra_Id == compra_id);
                if(compraEnvio == null)
                {
                    throw new Exception("No existe Envios para esta compra");
                }
                
                return compraEnvio.EnviarProducto(producto_id);
            }
            return false;
        }

        public bool EnviarCompra(int compra_id)
        {
            if (ComprobanteDePago.EstadoDePago == Enum.EstadoDePago.PAGADO)
            {
                CompraEnvio compraEnvio = CompraEnvios.ToList().Find(x => x.Compra_Id == compra_id);
                if (compraEnvio == null)
                {
                    throw new Exception("No existe Envios para esta compra");
                }
                return compraEnvio.EnviarProducto();
            }
            return false;
        }

    }
}
