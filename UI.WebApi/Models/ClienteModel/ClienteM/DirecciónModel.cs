﻿using System;
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
    public class DirecciónModel : ServicioDirección
    {
       
        public readonly IGenericRepository<Dirección> repository;
        public Dirección Dirección { set; get; }

        public DirecciónModel() : base(FactoriesSingleton<Dirección>.UnitOfWork, FactoriesSingleton<Dirección>.GenericRepository)
        {
            repository = FactoriesSingleton<Dirección>.GenericRepository;
        }


        public static DirecciónModel Instance
        {
            get
            {
                if (direcciónModel == null)
                {
                    direcciónModel = new DirecciónModel();
                }
                return direcciónModel;
            }
        }

        [JsonIgnore]
        private static DirecciónModel direcciónModel;

    }
}