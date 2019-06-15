using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    
    public class Persona : Entity<int>
    {
        public string documento { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string telefono { get; set; }
        public int edad { get; set; }

        public Persona()
        {

        }

        public Persona(string documento, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, int edad)
        {
            this.documento = documento;
            this.primerNombre = primerNombre;
            this.segundoNombre = segundoNombre;
            this.primerApellido = primerApellido;
            this.segundoApellido = segundoApellido;
            this.edad = edad;
        }

        public Persona(Persona persona)
        {
            this.documento = persona.documento;
            this.primerNombre = persona.primerNombre;
            this.segundoNombre = persona.segundoNombre;
            this.primerApellido = persona.primerApellido;
            this.segundoApellido = persona.segundoApellido;
            this.edad = persona.edad;
        
        }


       
        
    }
}
