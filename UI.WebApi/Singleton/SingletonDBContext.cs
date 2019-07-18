using Domain;
using Infraestructure.Data;
using Infraestructure.Data.Base;
using System;

namespace UI.WebApi.Singleton
{
    public static class SingletonDBContext
    {

       private static readonly Lazy<IDbContext> _db =
            new Lazy<IDbContext>(() => new DBContext());

        public static IDbContext Instance_db => _db.Value;

   
    }

}