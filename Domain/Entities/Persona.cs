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

        [Required]
        [MaxLength(10), MinLength(10)]
        [Column("documento")]
        public string documento;

        [Required]
        [MaxLength(50)]
        [Column("primerNombre")]
        public string primerNombre;

        [Column("segundoNombre")]
        public string segundoNombre;

        [Required]
        [MaxLength(50)]
        [Column("primerApellido")]
        public string primerApellido;

        [Required]
        [MaxLength(50)]
        [Column("segundoApellido")]
        public string segundoApellido;

        [Required]
        [MaxLength(10), MinLength(10)]
        [Column("telefono")]
        public string telefono;

        [Required]
        [MaxLength(3)]
        [Column("edad")]
        public int edad;
    }
}
