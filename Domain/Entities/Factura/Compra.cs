using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual IEnumerable<ProductoCliente> productos { set; get; }

        public int Comprobante_De_Pago_Id { set; get; }
        [ForeignKey("Comprobante_De_Pago_Id")]  public ComprobanteDePago ComprobanteDePago { set; get; }


    }
}
