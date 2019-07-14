using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Implements.Cliente.ServicioCliente;
using Domain.Abstracts;
using Domain.Entities.Cliente;
using Newtonsoft.Json;
using UI.WebApi.Singleton;

namespace UI.WebApi.Models.ClienteModel.ClienteM
{
    public class TelefónoModel : ServicioTelefóno
    {
        public readonly ServicioTelefóno servicioTelefóno;
        public readonly IGenericRepository<Telefóno> repository;
        public Telefóno Telefóno { set; get; }

        public TelefónoModel() : base(FactoriesSingleton<Telefóno>.UnitOfWork, FactoriesSingleton<Telefóno>.GenericRepository)
        {
            repository = FactoriesSingleton<Telefóno>.GenericRepository;
        }


        public static TelefónoModel Instance
        {
            get
            {
                if (telefónoModel == null)
                {
                    telefónoModel = new TelefónoModel();
                }
                return telefónoModel;
            }
        }

        [JsonIgnore]
        private static TelefónoModel telefónoModel;

    }
}