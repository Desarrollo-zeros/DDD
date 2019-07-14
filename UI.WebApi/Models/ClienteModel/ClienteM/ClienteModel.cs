
using Application.Implements.Cliente.ServicioCliente;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using UI.WebApi.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Application.Implements.Cliente.ServicioUsuario;
using UI.WebApi.Models.ClienteModel.UsuarioM;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class ClienteModel : ServicioCliente
    {

        public Cliente Cliente { set; get; }

        public static ClienteModel Instance 
        {
            get
            {
                if (clienteModel == null)
                {
                    clienteModel = new ClienteModel();
                }
                return clienteModel;
            }
        }

        private static ClienteModel clienteModel;

        public static ClienteModel GetAll(int id)
        {
            Instance.Cliente = Instance.Get(new ServicioClienteRequest { Usuario_Id = id });
            Instance.Cliente.Usuario = UsuarioModel.Instance.Get(new ServicioUsuarioRequest { Id = id });
            Instance.Cliente.Usuario.Password = "secreta";
            Instance.Cliente.Direcciónes = DirecciónModel.Instance.Get(new ServicioDireccíonRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.Telefónos = TelefónoModel.Instance.Get(new ServicioTelefónoRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.ClienteMetodoDePagos = MetodoPagoModel.Instance.GetAll(new ServicioMetodoPagoRequest { Cliente_Id = Instance.Cliente.Id });
            Instance.Cliente.CompraClientes = null;
            return Instance;
        }

        public static void SetAll(Cliente cliente)
        {
            Instance.Cliente = cliente;
            Instance.Cliente.Usuario = cliente.Usuario;
            Instance.Cliente.Usuario.Password = "secreta";
            Instance.Cliente.Direcciónes = cliente.Direcciónes;
            Instance.Cliente.Telefónos = cliente.Telefónos;
            Instance.Cliente.ClienteMetodoDePagos = cliente.ClienteMetodoDePagos;
            Instance.Cliente.CompraClientes = null;
        }
      
        [JsonIgnore]
        public readonly TelefónoModel telefónoModel;
        [JsonIgnore]
        public readonly DirecciónModel direcciónModel;

        [JsonIgnore]
        public readonly IGenericRepository<Cliente> repository;

        public ClienteModel() : base(FactoriesSingleton<Cliente>.UnitOfWork, FactoriesSingleton<Cliente>.GenericRepository)
        {
            repository = FactoriesSingleton<Cliente>.GenericRepository;
            telefónoModel = new TelefónoModel();
            direcciónModel = new DirecciónModel();
        }
    }
}