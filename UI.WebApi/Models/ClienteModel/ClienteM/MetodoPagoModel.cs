using Application.Implements.Cliente.ServicioCliente;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.WebApi.Singleton;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class MetodoPagoModel : ServicioMetodoPago
    {

        public readonly IGenericRepository<ClienteMetodoDePago> repository;

        public IEnumerable<ClienteMetodoDePago> MetodoDePagos { set; get; }

        public MetodoPagoModel() : base(FactoriesSingleton<ClienteMetodoDePago>.UnitOfWork, FactoriesSingleton<ClienteMetodoDePago>.GenericRepository)
        {
            repository = FactoriesSingleton<ClienteMetodoDePago>.GenericRepository;
        }


        public static MetodoPagoModel Instance
        {
            get
            {
                if (metodoPagoModel == null)
                {
                    metodoPagoModel = new MetodoPagoModel();
                }
                return metodoPagoModel;
            }
        }

        [JsonIgnore]
        private static MetodoPagoModel metodoPagoModel;

        public static MetodoPagoModel GetAll(int idUsuario)
        {
            ClienteModel.Instance.Cliente = ClienteModel.GetAll(idUsuario).Cliente;
            Instance.MetodoDePagos = ClienteModel.Instance.Cliente.ClienteMetodoDePagos;
            return Instance;
        }

        public static MetodoPagoModel Get(int idUsuario, int idMetodoPago)
        {
            ClienteModel.Instance.Cliente = ClienteModel.GetAll(idUsuario).Cliente;
            Instance.MetodoDePagos = ClienteModel.Instance.Cliente.ClienteMetodoDePagos.Where(x=>x.Id == idMetodoPago);
            return Instance;
        }
    }
}