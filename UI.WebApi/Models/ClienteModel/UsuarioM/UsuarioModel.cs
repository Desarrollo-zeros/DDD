using Application.Implements.Cliente.ServicioUsuario;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using UI.WebApi.Singleton;
using UI.WebApi.Models.ClienteModel.ClienteM;
using Application.Implements.Cliente.ServicioCliente;

namespace UI.WebApi.Models.ClienteModel.UsuarioM
{
    public class UsuarioModel : ServicioUsuario
    {

        public Usuario Usuario { set; get; }
       

        [JsonIgnore]
        public readonly IGenericRepository<Usuario> repository;
        [JsonIgnore]
        public readonly int id = 0;


        public static UsuarioModel Instance
        {
            get
            {
                if (usuarioModel == null)
                {
                    usuarioModel = new UsuarioModel();
                }
                return usuarioModel;
            }
        }

        [JsonIgnore]
        private static UsuarioModel usuarioModel;

        public UsuarioModel() : base(FactoriesSingleton<Usuario>.UnitOfWork, FactoriesSingleton<Usuario>.GenericRepository)
        {
            repository = FactoriesSingleton<Usuario>.GenericRepository;

            if (Auth().IsAuthenticated)
            {
                id = GetId(new ServicioUsuarioRequest { Username = Auth().Name });
            }

            
        }
        public IIdentity Auth()
        {
            return Thread.CurrentPrincipal.Identity;
        }

        public static UsuarioModel Get(int id)
        {
            Instance.Usuario = Instance.Get(new ServicioUsuarioRequest { Id = id });
            Instance.Usuario.Password = "Secret";
            return Instance;
        }


    }
}