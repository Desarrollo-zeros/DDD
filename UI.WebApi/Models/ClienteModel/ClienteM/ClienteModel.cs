
using Application.Implements.Cliente.ServicioCliente;
using Application.Implements.Cliente.ServicioUsuario;
using Domain.Entities.Cliente;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class ClienteModel : Model<Cliente>
    {

        public Cliente Cliente { set; get; }

        public static ClienteModel Instance
        {
            get
            {
                return new ClienteModel();
            }
        }



        public static ClienteModel GetAll(int id)
        {
            Instance.Cliente = Instance.ServicioCliente.Get(new ServicioClienteRequest { Usuario_Id = id });
            Instance.Cliente.Usuario = Instance.ServicioUsuario.Get(new ServicioUsuarioRequest { Id = id });
            Instance.Cliente.Usuario.Password = "secreta";
            Instance.Cliente.Direcciónes = Instance.ServicioDirección.Get(new ServicioDireccíonRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.Telefónos = Instance.ServicioTelefóno.Get(new ServicioTelefónoRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.ClienteMetodoDePagos = Instance.ServicioMetodoPago.GetAll(new ServicioMetodoPagoRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.CompraClientes = null;
            return Instance;
        }


        public ClienteModel()
        {

        }
    }
}