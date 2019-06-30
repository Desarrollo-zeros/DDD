using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Producto
{
    public class Descuento : Entity<int>
    {
        public Descuento(TipoDescuento tipoDescuento, bool acomulable, DateTime fechaYHoraInicio, DateTime fechaYHoraTerminación)
        {
            this.tipoDescuento = tipoDescuento;
            Acomulable = acomulable;
            FechaYHoraInicio = fechaYHoraInicio;
            FechaYHoraTerminación = fechaYHoraTerminación;
        }

        public Descuento() { }

        public TipoDescuento tipoDescuento { set; get; }

        public bool Acomulable { set; get; }
        
        public DateTime FechaYHoraInicio { set; get; }
        public DateTime FechaYHoraTerminación { set; get; }

        public virtual IEnumerable<ProductoDescuento> ProductoDescuentos { set; get; }
    }
}
