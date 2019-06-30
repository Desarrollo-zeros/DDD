using Domain.Enum;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Factura
{
    [Table("Comprobante_De_Pago")]
    public class ComprobanteDePago : Entity<int>
    {
        public ComprobanteDePago(EstadoDePago estadoDePago, double total, double subTotal, Pago pago, DateTime fechaDePago, TotalDescuentoAplicados totalDescuentoAplicados)
        {
            EstadoDePago = estadoDePago;
            Total = total;
            SubTotal = subTotal;
            Pago = pago;
            FechaDePago = fechaDePago;
            TotalDescuentoAplicados = totalDescuentoAplicados;
        }

        public ComprobanteDePago() { }

        public EstadoDePago EstadoDePago { set; get; }
        public double Total { set; get; }
        public double SubTotal { set; get; }
         
        public Pago Pago { set; get; }

        public DateTime FechaDePago { set; get; }

        public TotalDescuentoAplicados TotalDescuentoAplicados { set; get; }

        
    }
}
