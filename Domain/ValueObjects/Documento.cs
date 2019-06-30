using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ValueObjects
{
    public class Documento
    {
        public Documento(){}
        public Documento(string documento) {
            if (this.EsDocumento(documento))
            {
                this.Numero = documento;
            }
        }
       
        private bool EsDocumento(string documento) { return (documento.Length == 8 || documento.Length == 10); }

        [Column("Documento")]
        public string Numero { set; get; }
    }
}
