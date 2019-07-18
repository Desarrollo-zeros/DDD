using Application.Base;
using Application.Implements.Cliente.ServicioCliente;
using Application.Implements.Cliente.ServicioUsuario;
using Application.Implements.Factura;
using Domain;
using Domain.Entities.Cliente;
using Domain.Entities.Factura;
using Newtonsoft.Json;
using UI.WebApi.Singleton;

namespace UI.WebApi.Models
{
    public abstract class Model<T> : EntityService<T> where T : BaseEntity
    {
        [JsonIgnore]
        public ServicioUsuario ServicioUsuario { set; get; }
        [JsonIgnore]
        public ServicioCliente ServicioCliente { set; get; }
        [JsonIgnore]
        public ServicioDirección ServicioDirección { set; get; }
        [JsonIgnore]
        public ServicioTelefóno ServicioTelefóno { set; get; }
        [JsonIgnore]
        public ServicioMetodoPago ServicioMetodoPago { set; get; }
        [JsonIgnore]
        public ServicioCompra ServicioCompra { set; get; }
        [JsonIgnore]
        public ServicioCompraCliente ServicioCompraCliente { set; get; }




        public Model() : base(FactoriesSingleton<T>.UnitOfWork, FactoriesSingleton<T>.GenericRepository)
        {
            ServicioUsuario = new ServicioUsuario(FactoriesSingleton<Usuario>.UnitOfWork, FactoriesSingleton<Usuario>.GenericRepository);
            ServicioCliente = new ServicioCliente(FactoriesSingleton<Cliente>.UnitOfWork, FactoriesSingleton<Cliente>.GenericRepository);
            ServicioDirección = new ServicioDirección(FactoriesSingleton<Dirección>.UnitOfWork, FactoriesSingleton<Dirección>.GenericRepository);
            ServicioTelefóno = new ServicioTelefóno(FactoriesSingleton<Telefóno>.UnitOfWork, FactoriesSingleton<Telefóno>.GenericRepository);
            ServicioMetodoPago = new ServicioMetodoPago(FactoriesSingleton<ClienteMetodoDePago>.UnitOfWork, FactoriesSingleton<ClienteMetodoDePago>.GenericRepository);
            ServicioCompra = new ServicioCompra(FactoriesSingleton<Compra>.UnitOfWork, FactoriesSingleton<Compra>.GenericRepository);
            ServicioCompraCliente = new ServicioCompraCliente(FactoriesSingleton<CompraCliente>.UnitOfWork, FactoriesSingleton<CompraCliente>.GenericRepository);

        }

    }
}