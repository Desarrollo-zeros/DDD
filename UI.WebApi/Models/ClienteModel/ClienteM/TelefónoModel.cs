using Domain.Entities.Cliente;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class TelefónoModel : Model<Telefóno>
    {

        public Telefóno Telefóno { set; get; }

        public TelefónoModel()
        {
        }

        public static TelefónoModel Instance
        {
            get
            {
                return new TelefónoModel();
            }
        }



    }
}