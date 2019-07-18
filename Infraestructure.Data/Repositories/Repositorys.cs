using Domain;
using Infraestructure.Data.Base;


namespace Infraestructure.Data.Repositories
{
    public class Repository<T> : GenericRepository<T> where T : BaseEntity
    {
        private readonly IDbContext Context;
        public Repository(IDbContext context) : base(context) { Context = context; }

        public T Add(T t, bool saveChange)
        {

            Add(t);
            if (saveChange) { Context.SaveChanges(); }
            return t;
        }

        public T Edit(T t, bool saveChange)
        {
            Edit(t);
            if (saveChange) { Context.SaveChanges(); }
            return t;
        }

        public T Delete(T t, bool saveChange)
        {
            Delete(t);
            if (saveChange) { Context.SaveChanges(); }
            return t;
        }




    }
}

