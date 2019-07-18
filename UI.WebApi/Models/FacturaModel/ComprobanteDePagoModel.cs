using Domain.Entities.Factura;

namespace UI.WebApi.Models.FacturaModel
{
    public class ComprobanteDePagoModel : Model<ComprobanteDePago>
    {
        public ComprobanteDePago ComprobanteDePago { set; get; }


        public static ComprobanteDePagoModel Instance
        {
            get
            {
                return new ComprobanteDePagoModel();
            }
        }

    }
}