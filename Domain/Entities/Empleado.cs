using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Empleado : Entity<int>
    {
        [Column("salario")]
        public double salario { private set; get; }

        [Column("dias")]
        public int dias { private set; get; }

        public int año { private set; get; }




        public Empleado(Empleado empleado) : base() {
            this.salario = empleado.salario;
            this.dias = empleado.dias;
            this.año = empleado.año;
        }


        public Empleado(double salario, int dias, int año) : base()
        {
            this.salario = salario;
            this.dias = dias;
            this.año =año;
        }


     

        public Empleado() : base() { }

    }
}
