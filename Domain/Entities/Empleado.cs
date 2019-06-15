using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Empleado : Persona
    {
        [Column("salario")]
        public double salario { private set; get; }

        [Column("dias")]
        public int dias { private set; get; }

        public int año { private set; get; }


        [Display(Name = "Persona")]
        public int personaId { get; set; }

        [ForeignKey("Persona")]
        public virtual Persona persona { get; set; }


        public Empleado(Empleado empleado) : base(empleado) {
            this.salario = empleado.salario;
            this.dias = empleado.dias;
            this.año = empleado.año;
        }

        public Empleado() : base() { }

    }
}
