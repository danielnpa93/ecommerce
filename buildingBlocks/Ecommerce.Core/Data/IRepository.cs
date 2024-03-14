using System;
using Ecommerce.Core.DomainObjects;

namespace Ecommerce.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}