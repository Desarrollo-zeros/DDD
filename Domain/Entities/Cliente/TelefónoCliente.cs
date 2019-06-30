using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Cliente
{
    [Table("Telefóno_Cliente")]
    public class TelefónoCliente : Entity<int>
    {
        public TelefónoCliente(int cliente_Id, int telefóno_Id)
        {
            Cliente_Id = cliente_Id;
            Telefóno_Id = telefóno_Id;
        }

        public TelefónoCliente() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")]  public Cliente Cliente { set; get; }
        public int Telefóno_Id { set; get; }
        [ForeignKey("Telefóno_Id")]  public Telefóno Telefóno { set; get; }
    }
}
