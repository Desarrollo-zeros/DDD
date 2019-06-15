using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface IServicioFinanciero
    {
        void calcular(double salario, int dias, int año = 360);
    }
}
