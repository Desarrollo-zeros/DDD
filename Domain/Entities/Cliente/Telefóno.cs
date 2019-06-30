using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities.Cliente
{
    public class Telefóno : Entity<int>
    {
        public Telefóno(Teléfono número, TipoTelefono tipoTelefono)
        {
            Número = número;
            TipoTelefono = tipoTelefono;
        }

        public Telefóno() { }

        public Domain.ValueObjects.Teléfono Número { set; get; }
        public TipoTelefono TipoTelefono { set; get; }
        public virtual IEnumerable<TelefónoCliente> Telefónos { set; get; }
    }

    
}
