using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ValueObjects
{
    public class Teléfono
    {
        public Teléfono() { }
        public Teléfono(string telefono) {
            this.Tel = telefono;
        }

        [Column("Telefóno")]
        public string Tel { set; get; }

        //indicador pais
        //telefono +57 - 3043541475
        //numero Celular

        //Telefono 5779797 -> telefono Fijo

        public bool EsFijo()
        {
            return (Tel.Length == 7);
        }


        public bool EsCelular()
        {
            return (Tel.Length == 10);
        }

        //ejemplo1, 0 - 3043541475
        //ejemplo2, 1 - 5779797

        public string ConstruirTelefono() {
            return (EsCelular()) ? TipoTelefono.CELULAR + "-" + Tel : TipoTelefono.FIJO + "-" + Tel; 
        }

        //[0] => TipoTelefono, [1] => Telefono
        public string[] DescomponerTelefono()
        {
            return Tel.Split('-');
        }
        
    }
}

