using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class Prima : Entity<int>, IServicioFinanciero
    {

        public double prima { private set; get; }
        public DateTime fechaCalculoPrima { private set; get; }


        [Display(Name = "Empleado")]
        public int empleadoId { get; set; }

        [ForeignKey("Empleado")]
        public virtual Empleado empleado { get; set; }


        public void calcular(double salario, int dias, int año = 360)
        {
            if (salario <= 0) { throw new CalcularPrimaException("El salario no puede ser menor o igual a 0"); }
            if (dias <= 0){ throw new CalcularPrimaException("los dias no puede ser menor o igual a 0"); }
            this.prima = (salario * dias) / año;
        }
    }
}
