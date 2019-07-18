using Domain;
using Domain.Abstracts;

namespace UI.WebApi.Singleton
{
    public static class FactoriesSingleton<T> where T : BaseEntity
    {
        public static IUnitOfWork UnitOfWork = SingletonUnitWork<T>.UnitOfWork;
        public static IGenericRepository<T> GenericRepository = SingletonRepository<T>.GenericRepository;
    }

}