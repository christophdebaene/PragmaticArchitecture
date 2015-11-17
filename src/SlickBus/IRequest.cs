using System;

namespace SlickBus
{
    public interface IRequest<TResponse>
    {
        Guid Id { get; }
    }
}