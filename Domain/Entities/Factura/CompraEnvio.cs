using Domain.Entities.Localizacíon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;

namespace Domain.Entities.Factura
{
    [Table("Compra_Envio")]
    public class CompraEnvio : Entity<int>
    {
        public CompraEnvio(int compra_Id, int dirección_Id, DateTime fechaEnvio, DateTime fechaLLegada, EstadoDeEnvio estadoDeEnvio)
        {
            Compra_Id = compra_Id;
            Dirección_Id = dirección_Id;
            FechaEnvio = fechaEnvio;
            FechaLLegada = fechaLLegada;
            EstadoDeEnvio = estadoDeEnvio;
        }

        public CompraEnvio() { }

        public int Compra_Id { set; get; }
        [ForeignKey("Compra_Id")]  public Compra Compra { set; get; }

        public int Dirección_Id { set; get; }
        [ForeignKey("Dirección_Id")]  public Dirección Dirección { set; get; }

        public DateTime FechaEnvio { set; get; }
        public DateTime FechaLLegada { set; get; }
        public EstadoDeEnvio EstadoDeEnvio { set; get; }
    }
}
