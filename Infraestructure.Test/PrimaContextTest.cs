using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using System.Data.Entity;
using Domain.Entities;

namespace Infraestructure.Test
{
    public class PrimaContextTest : PrimaContext
    {
        public PrimaContextTest() : base()
        {
        }

        protected PrimaContextTest(DbConnection connection) : base(connection)
        {
            Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<PrimaContextTest>(new AlwaysCreateInitializer());


            base.OnModelCreating(modelBuilder);
        }

        public void Seed(PrimaContext context)
        {
            Persona persona = new Persona("1063969856","carlos", "andres", "castilla", "garcia", 21);
            context.personas.Add(persona);  
            context.SaveChanges();
        }

    }

    public class CreateInitializer : CreateDatabaseIfNotExists<PrimaContextTest>
    {
        protected override void Seed(PrimaContextTest context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }


    public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<PrimaContextTest>
    {
        protected override void Seed(PrimaContextTest context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

    public class AlwaysCreateInitializer : DropCreateDatabaseAlways<PrimaContextTest>
    {
        protected override void Seed(PrimaContextTest context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

}
