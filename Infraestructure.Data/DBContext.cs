﻿using Domain.Entities.Cliente;
using Domain.Entities.Factura;
using Domain.Entities.Localizacíon;
using Domain.Entities.Producto;
using Infraestructure.Data.Base;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infraestructure.Data
{
    public class DBContext : DbContextBase
    {
        public DBContext() : base("name=DBContext") { }

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

        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraEnvio> CompraEnvios { get; set; }
        public DbSet<ComprobanteDePago> ComprobanteDePagos { get; set; }
        public DbSet<CompraCliente> CompraClientes { get; set; }

        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoDescuento> ProductoDescuentos { get; set; }

        public DbSet<ClienteMetodoDePago> ClienteMetodoDePagos { get; set; }

        public DbSet<ClienteMetodoHistoriaDePago> ClienteMetodoHistoriaDePagos { set; get; }

        public DbSet<CompraEnvioProducto> CompraEnvioProductos { set; get; }

        public DbSet<Categoria> Categorias { set; get; }
    }
}
