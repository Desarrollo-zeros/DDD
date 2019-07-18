using Application.Implements.Cliente.ServicioUsuario;
using Domain.Entities.Cliente;
using Domain.Enum;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Threading;

namespace UI.WebApi.Models.ClienteModel.UsuarioM
{
    public class UsuarioModel : Model<Usuario>
    {

        public Usuario Usuario { set; get; }

        [JsonIgnore]
        public readonly int id = 0;
        public readonly Rol rol;




        public static UsuarioModel Instance
        {
            get
            {
                return new UsuarioModel();
            }
        }



        public UsuarioModel()
        {
            if (Auth().IsAuthenticated)
            {
                var usuario = ServicioUsuario.Get(new ServicioUsuarioRequest { Username = Auth().Name });
                id = usuario.Id;
                rol = usuario.Rol;
            }


        }
        public IIdentity Auth()
        {
            return Thread.CurrentPrincipal.Identity;
        }

        public static UsuarioModel Get(int id)
        {
            Instance.Usuario = Instance.ServicioUsuario.Get(new ServicioUsuarioRequest { Id = id });
            Instance.Usuario.Password = "Secret";
            return Instance;
        }
    }
}