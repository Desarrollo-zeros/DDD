using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.ValueObjects
{
    public class TotalDescuentoAplicados
    {
        public TotalDescuentoAplicados(double[] descuento)
        {
           for(int i=0; i < descuento.Length; i++)
           {
                this.Descuento += descuento[i];
           }
        }

        public TotalDescuentoAplicados()
        {

        }


        [Column("Total_Descuento_Aplicado")]
        public double Descuento { set; get; }
    }
}