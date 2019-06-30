using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Localizacíon;

namespace Domain.Entities.Cliente
{
    [Table("Cliente_Dirección")]
    public class ClienteDireccíon : Entity<int>
    {
        public ClienteDireccíon(int cliente_Id, int direccíon_Id)
        {
            Cliente_Id = cliente_Id;
            Direccíon_Id = direccíon_Id;
        }

        public ClienteDireccíon() { }

        public int Cliente_Id { set; get; }
        [ForeignKey("Cliente_Id")] public Cliente Cliente { set; get; }
        public int Direccíon_Id { set; get; }
        [ForeignKey("Direccíon_Id")] public Dirección Dirección { set; get; }
    }
}
