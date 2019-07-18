using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entities.Factura
{
    public class Compra : Entity<int>
    {
        public Compra(int cliente_Id, DateTime fechaCompra)
        {
            Cliente_Id = cliente_Id;
            FechaCompra = fechaCompra;
        }

        public Compra() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")] public Cliente.Cliente Cliente { set; get; }
        public DateTime FechaCompra { set; get; }
        public virtual IEnumerable<CompraCliente> CompraClientes { set; get; }

        public virtual IEnumerable<CompraEnvio> CompraEnvios { set; get; }

        public virtual IEnumerable<ComprobanteDePago> ComprobanteDePagos { set; get; }

        [NotMapped]
        public int Cantidad { set; get; }





        public bool CompletarCompras()
        {
            double descuentoTotal = 0;
            double precioVenta = 0;



            if (CompraClientes.Count() == 0)
            {
                throw new Exception("No hay productos para realizar la compra");
            }


            CompraClientes.ToList().ForEach(x =>
            {
                descuentoTotal += ObtenerDescuentoPorProductoCompra(x.Producto_Id, x.Cantidad);
                precioVenta += x.Producto.PrecioVenta * x.Cantidad;
                x.EstadoClienteArticulo = Enum.EstadoClienteArticulo.PAGADO;
            });

            if (!DescontarTotalProductoEnSaldo((precioVenta - descuentoTotal)))
            {
                throw new Exception("No tien saldo suficiente para realizar la compra");
            }
            return true;
        }

        public bool DescontarTotalProductoEnSaldo(double saldo)
        {

            if (saldo < 1)
            {
                return false;
            }
            if (Cliente.ClienteMetodoDePagos != null)
            {
                if (Cliente.ClienteMetodoDePagos.ToList().Find(x => x.Activo && x.Saldo > saldo).DescontarSaldo(saldo))
                {
                    ComprobanteDePagos.ToList().Find(x => x.Compra_Id == Id).EstadoDePago = Enum.EstadoDePago.PAGADO;
                    return true;
                }
            }
            return false;
        }


        public double ObtenerDescuentoPorProductoCompra(int producto_Id, int cantidad)
        {
            var sumaDescuento = 0.0;
            var valorProducto = 0.0;
            if (CompraClientes != null)
            {
                var producto = CompraClientes.ToList().Find(x => x.Producto_Id == producto_Id).Producto;
                if (producto != null)
                {
                    if (producto.ProductoDescuentos != null)
                    {
                        producto.ProductoDescuentos.ToList().ForEach(x =>
                        {
                            if (x.Descuento != null)
                            {
                                if (x.Descuento.DescuentoEsAplicable(x.Descuento.FechaYHoraInicio, x.Descuento.FechaYHoraTerminación))
                                {
                                    sumaDescuento += x.Descuento.Descu;
                                }
                            }
                        });
                    }
                    else
                    {
                        throw new Exception("Producto Descuento esta vacio");
                    }
                    valorProducto = producto.PrecioVenta;
                }
                else
                {
                    throw new Exception("Producto esta vacio");
                }
                return valorProducto * cantidad * sumaDescuento;
            }
            else
            {
                throw new Exception("Compra cliente esta vacio");
            }
        }

        public bool SePuedeComprarProducto(int CantidadProducto, int cantidad)
        {
            return CantidadProducto > cantidad;
        }

        public bool EnviarCompra(int producto_id)
        {
            if (ComprobanteDePagos != null)
            {
                if (ComprobanteDePagos.ToList().Find(x => x.Compra_Id == Id).EstadoDePago == Enum.EstadoDePago.PAGADO)
                {
                    return CompraEnvios.ToList().Find(x => x.Compra_Id == Id).EnviarProducto(producto_id);
                }
            }
            return false;
        }

        public bool EnviarCompra()
        {
            var comprobanteDe = ComprobanteDePagos.ToList().Find(x => x.Compra_Id == Id);
            if (comprobanteDe == null)
            {
                throw new Exception("No existe Un Estado De pago");
            }
            if (comprobanteDe.EstadoDePago == Enum.EstadoDePago.PAGADO)
            {
                return CompraEnvios.ToList().Find(x => x.Compra_Id == Id).EnviarProducto();
            }
            return false;
        }

        public double ObtenerTotal()
        {
            double descuentoTotal = 0;
            double precioVenta = 0;
            if (CompraClientes != null)
            {
                CompraClientes.ToList().ForEach(x =>
                {
                    if (x.Compra_Id == Id)
                    {
                        descuentoTotal += ObtenerDescuentoPorProductoCompra(x.Producto_Id, x.Cantidad);
                        precioVenta += x.Producto.PrecioVenta * x.Cantidad;
                    }
                });
            }
            return precioVenta - descuentoTotal;
        }

        public double ObtenerSubTotal()
        {
            double precioVenta = 0;
            if (CompraClientes != null)
            {
                CompraClientes.ToList().ForEach(x =>
                {
                    if (x.Compra_Id == Id)
                    {
                        precioVenta += x.Producto.PrecioVenta * x.Cantidad;
                    }
                });
            }
            return precioVenta;
        }

        public double ObtenerDescuento()
        {
            double descuento = 0;
            CompraClientes.ToList().ForEach(x =>
            {
                if (x.Compra_Id == Id)
                {
                    descuento += ObtenerDescuentoPorProductoCompra(x.Producto_Id, x.Cantidad);
                }

            });
            return descuento;
        }

    }
}
