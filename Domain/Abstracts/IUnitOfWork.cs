using System;

namespace Domain.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
