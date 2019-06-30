using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Entities;
using Infraestructure.Data.Base;
using Domain.Entities.Cliente;
using Domain.Entities.Localizacíon;
using Domain.Entities.Factura;
using Domain.Entities.Producto;

namespace Infraestructure.Data
{
    public class DBContext : DbContextBase
    {

        
        public DBContext() : base("name=DBContext") {}
       
        protected DBContext(DbConnection connection) : base(connection) { }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<País> País { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Dirección> Direccións { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Telefóno> Telefónos { get; set; }
        public DbSet<TelefónoCliente> telefónoCLientes { get; set; }
        public DbSet<ClienteDireccíon> ClienteDireccíons { get; set; }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraEnvio> CompraEnvios { get; set; }
        public DbSet<ComprobanteDePago> comprobanteDePagos { get; set; }
        public DbSet<ProductoCliente> ProductoClientes { get; set; }

        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoDescuento> ProductoDescuentos { get; set; }

        public DbSet<ClienteMetodoDePago> ClienteMetodoDePagos { get; set; }

        public DbSet<ClienteMetodoHistoriaDePago> ClienteMetodoHistoriaDePagos { set; get; }
    }
}
