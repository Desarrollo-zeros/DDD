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

namespace Infraestructure.Data
{
    public class PrimaContext : DbContextBase
    {
        public PrimaContext() : base("Name=mysqlContext") {}
       
        protected PrimaContext(DbConnection connection) : base(connection) { }

        public DbSet<Persona> personas { get; set; }
        public DbSet<Empleado> empleados { get; set; }
        public DbSet<Prima> primas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
