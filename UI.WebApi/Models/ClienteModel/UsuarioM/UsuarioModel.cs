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

namespace UI.WebApi.Models.ClienteModel.UsuarioM
{
    public class UsuarioModel : ServicioUsuario
    {

        public Usuario Usuario { set; get; }

        public readonly ServicioUsuario servicioUsuario;
        public readonly IGenericRepository<Usuario> repository;
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

    }
}