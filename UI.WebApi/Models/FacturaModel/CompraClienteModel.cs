using Domain.Entities.Factura;

namespace UI.WebApi.Models.FacturaModel
{
    public class CompraClienteModel : Model<CompraCliente>
    {

        public static CompraClienteModel Instance
        {
            get
            {
                return new CompraClienteModel();
            }
        }
    }
}