using Domain.Entities.Cliente;
using System.Collections.Generic;
using System.Linq;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class MetodoPagoModel : Model<ClienteMetodoDePago>
    {
        public IEnumerable<ClienteMetodoDePago> ClienteMetodoDePagos { set; get; }

        public MetodoPagoModel()
        {

        }

        public static MetodoPagoModel Instance
        {
            get
            {
                return new MetodoPagoModel();
            }
        }



        public static MetodoPagoModel GetAll(int idUsuario)
        {
            Instance.ClienteMetodoDePagos = ClienteModel.GetAll(idUsuario).Cliente.ClienteMetodoDePagos;
            return Instance;
        }

        public static MetodoPagoModel Get(int idUsuario, int idMetodoPago)
        {
            Instance.ClienteMetodoDePagos = ClienteModel.GetAll(idUsuario).Cliente.ClienteMetodoDePagos.Where(x => x.Id == idMetodoPago);
            return Instance;
        }
    }
}