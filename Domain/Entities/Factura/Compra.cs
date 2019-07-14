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
        public Compra(int cliente_Id, DateTime fechaCompra)
        {
            Cliente_Id = cliente_Id;
            FechaCompra = fechaCompra;

            CantidadProductoNoExistentes = 0;
        }

        public Compra() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")]  public Cliente.Cliente Cliente { set; get; }
        public DateTime FechaCompra { set; get; }
        public virtual IEnumerable<CompraCliente> ProductoCliente { set; get; }

        public virtual IEnumerable<CompraEnvio> CompraEnvios { set; get; }
      
         public virtual IEnumerable<ComprobanteDePago>  ComprobanteDePagos { set; get; }


        [NotMapped]
        public int CantidadProductoNoExistentes { private set; get; }


        public bool ComprarArticulos()
        {
            double descuentoTotal = 0;
            double precioVenta = 0;

            int compra_id = 0;

            if (ProductoCliente.Count() == 0)
            {
                throw new Exception("No hay productos para realizar la compra");
            }
            foreach (var p in ProductoCliente)
            {
                descuentoTotal += ObtenerDescuentoPorProductoCompra(p.Cliente_Id, p.Producto_Id, p.Cantidad);
                precioVenta += p.Producto.PrecioVenta*p.Cantidad;
                p.EstadoProductoCliente = Enum.EstadoClienteArticulo.PAGADO;
                compra_id = p.Compra_Id;
            }

            if (!this.DescontarTotalProductoEnSaldo((precioVenta - descuentoTotal), compra_id))
            {
                throw new Exception("No tien saldo suficiente para realizar la compra");
            }

            return true;
        }

        public bool DescontarTotalProductoEnSaldo(double saldo, int compra_id)
        {

            if(compra_id == 0 || saldo == 0)
            {
                return false;
            }

            foreach (ClienteMetodoDePago pagos in Cliente.ClienteMetodoDePagos.ToList())
            {
                if (pagos.Activo && pagos.Saldo > saldo)
                {
                    if (pagos.DescontarSaldo(saldo))
                    {
                        ComprobanteDePagos.ToList().Find(x=>x.Compra_Id == compra_id).EstadoDePago = Enum.EstadoDePago.PAGADO;
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

            foreach(CompraCliente productoCliente in ProductoCliente)
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
            if(ComprobanteDePagos.ToList().Find(x => x.Compra_Id == compra_id).EstadoDePago == Enum.EstadoDePago.PAGADO)
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
            var ComprobanteDePagosL = ComprobanteDePagos.ToList().Find(x => x.Compra_Id == compra_id);
            if(ComprobanteDePagosL == null)
            {
                throw new Exception("No existe Un Estado De pago");
            }

            if (ComprobanteDePagos.ToList().Find(x => x.Compra_Id == compra_id).EstadoDePago == Enum.EstadoDePago.PAGADO)
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
