using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Persona")]
    public class Persona : Entity<int>
    {


        public Persona()
        {

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

        [Required]
        [MaxLength(10), MinLength(10)]
        public string documento;

        [Required]
        [MaxLength(50)]
        public string primerNombre;

        public string segundoNombre;
        [Required]
        [MaxLength(50)]
        public string primerApellido;

        [Required]
        [MaxLength(50)]
        public string segundoApellido;

        [Required]
        [MaxLength(10), MinLength(10)]
        public string telefono;

        [Required]
        [MaxLength(3)]
        public int edad;
    }
}
