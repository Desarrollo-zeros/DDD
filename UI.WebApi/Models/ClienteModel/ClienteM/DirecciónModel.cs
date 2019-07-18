using Domain.Entities.Cliente;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class DirecciónModel : Model<Dirección>
    {

        public Dirección Dirección { set; get; }

        public DirecciónModel()
        {

        }


        public static DirecciónModel Instance
        {
            get
            {
                return new DirecciónModel();
            }
        }



    }
}