using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Cliente
{
    public class Telefóno : Entity<int>
    {
        public Telefóno(string número, TipoTelefono tipoTelefono, int cliente_id)
        {
            Número = número;
            TipoTelefono = tipoTelefono;
            Cliente_Id = cliente_id;
        }

        public Telefóno() { }

        [Column("Telefóno")]
        public string Número { set; get; }
        public TipoTelefono TipoTelefono { set; get; }
        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")] public Cliente Cliente { set; get; }


    }


}
